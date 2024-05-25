using ELibrary.Orders.Application.Interfaces;
using ELibrary.Orders.Domain.Entity;
using ELibrary.Orders.Domain.Interfaces;

namespace ELibrary.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAllOrders();
        }
    }
}
