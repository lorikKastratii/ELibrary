using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Application.Interfaces
{
    public interface IOrderService
    {
        public List<Order> GetOrders();
    }
}
