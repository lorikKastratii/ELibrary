using ELibrary.Orders.Application.Dtos;
using ELibrary.Orders.Application.Extensions;
using ELibrary.Orders.Application.Requests;
using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<ServiceResponse<OrderResultDto>> CreateOrderAsync(CreateOrderRequest request);
    }
}
