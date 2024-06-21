using ELibrary.Orders.Application.Dtos;
using ELibrary.Orders.Application.Requests;
using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Application.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Task<OrderResultDto> CreateOrderAsync(CreateOrderRequest request);
    }
}
