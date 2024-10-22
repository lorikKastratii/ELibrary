using AutoMapper;
using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Application.Dtos;
using ELibrary.Orders.Application.Extensions;
using ELibrary.Orders.Application.Extensions.Errors;
using ELibrary.Orders.Application.Interfaces;
using ELibrary.Orders.Application.Requests;
using ELibrary.Orders.Domain.Entity;
using ELibrary.Orders.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ELibrary.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        private readonly IBookClient _bookClient;
        private readonly IUserClient _userClient;

        public OrderService(
            IOrderRepository orderRepository,
            IBookClient bookClient,
            IMapper mapper,
            ILogger<OrderService> logger,
            IUserClient userClient)
        {
            _orderRepository = orderRepository;
            _bookClient = bookClient;
            _mapper = mapper;
            _logger = logger;
            _userClient = userClient;
        }

        public async Task<ServiceResponse<OrderResultDto>> CreateOrderAsync(CreateOrderRequest request)
        {
            var user = await _userClient.GetUserById(request.UserId);

            if (user is null)
            {
                _logger.LogError("Failed to create order because User with Id: {id} does not exists.", request.UserId);
                return ServiceResponse<OrderResultDto>.Failure(OrderErrors.UserNotFound);
            }

            var orderItems = new List<OrderItem>();
            var outOfStockItems = new List<OrderItemDto>();

            (orderItems, outOfStockItems) = await CalculateItemsAsync(orderItems, outOfStockItems, request);

            if (orderItems.Any() is false)
            {
                _logger.LogError("Currently we don't have stock for these items: {@items}", request.OrderItems);
                return ServiceResponse<OrderResultDto>.Failure(OrderErrors.StockEmpty);
            }

            var result = await CreateOrderAsync(request, orderItems);

            if (result is not null)
            {
                result.OutOfStockItems = outOfStockItems;
                //map and return success case
            }

            //TODO: Update Stock

            return ServiceResponse<OrderResultDto>.Failure(OrderErrors.InternalError);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        private decimal CalculateTotalAmount(List<OrderItem> orderItems)
        {
            decimal totalAmount = 0;

            foreach (var item in orderItems)
            {
                totalAmount += item.Quantity * item.UnitPrice;
            }

            return totalAmount;
        }

        private async Task<(List<OrderItem>, List<OrderItemDto>)> CalculateItemsAsync(List<OrderItem> orderItems, List<OrderItemDto> outOfStockItems, CreateOrderRequest request)
        {            
            foreach (var item in request.OrderItems)
            {
                var book = await _bookClient.GetBookAsync(item.BookId);

                if (book is null || book.StockQuantity == 0)
                {
                    _logger.LogWarning("Currently we don't have stock for item with Id: {Id}", item.BookId);
                    outOfStockItems.Add(new OrderItemDto { BookId = item.BookId, Quantity = item.Quantity });
                    continue;
                }

                var availableQuantity = book.StockQuantity;

                //10 || 17
                if (availableQuantity < item.Quantity)
                {
                    var unavailableQuantity = item.Quantity - availableQuantity;
                    outOfStockItems.Add(new OrderItemDto { BookId = item.BookId, Quantity = unavailableQuantity });
                }

                orderItems.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = availableQuantity,
                    UnitPrice = item.UnitPrice,
                    CreatedDate = DateTime.UtcNow
                });
            }

            return (orderItems, outOfStockItems);
        }

        private async Task<OrderResultDto> CreateOrderAsync(CreateOrderRequest request, List<OrderItem> orderItems)
        {
            var order = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.UtcNow,
                Status = Domain.Enums.OrderStatus.InProgress,
                ShippingAddress = request.ShippingAddress,
                ShippingCity = request.ShippingCity,
                ShippingState = request.ShippingState,
                ShippingPostalCode = request.ShippingPostalCode,
                ShippingCountry = request.ShippingCountry,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = null,
                OrderItems = orderItems,
                IsActive = true
            };

            order.TotalAmount = CalculateTotalAmount(orderItems);
            var result = await _orderRepository.CreateOrderAsync(order);

            if (result)
            {
                //TODO: update book stock
                return new OrderResultDto
                {
                    Order = new OrderDto
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        OrderDate = order.OrderDate,
                        Status = nameof(order.Status),
                        ShippingAddress = order.ShippingAddress,
                        ShippingCity = order.ShippingCity,
                        ShippingState = order.ShippingState,
                        ShippingPostalCode = order.ShippingPostalCode,
                        ShippingCountry = order.ShippingCountry,
                        OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                        {
                            BookId = oi.BookId,
                            Quantity = oi.Quantity,
                            UnitPrice = oi.UnitPrice
                        }).ToList(),
                        TotalAmount = order.TotalAmount
                    }
                };
            }
            return null;
        }
    }
}
