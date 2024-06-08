using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Task<bool> CreateOrderAsync(Order order);
    }
}
