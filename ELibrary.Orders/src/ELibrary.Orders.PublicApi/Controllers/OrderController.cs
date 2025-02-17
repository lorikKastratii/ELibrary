using AutoMapper;
using ELibrary.Orders.Application.Interfaces;
using ELibrary.Orders.Application.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Orders.PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly IValidator<CreateOrderRequest> _validator;

        public OrderController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<OrderController> logger, IValidator<CreateOrderRequest> validator)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();

            if (orders.Any())
            {
                return Ok(orders);
            }

            return Ok("There are no orders currently");
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid is false)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var response = await _orderService.CreateOrderAsync(request);

            if (response.IsSuccess is false)
            {
                return Ok(response.Error?.Message);
            }

            return Ok("Order Created successfully!");
        }
    }
}
