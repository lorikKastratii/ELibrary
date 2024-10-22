using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<bool> CreateOrderAsync(Order order);
    }
}
