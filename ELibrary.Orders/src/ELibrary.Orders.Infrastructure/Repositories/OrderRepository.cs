using ELibrary.Orders.Domain.Entity;
using ELibrary.Orders.Domain.Interfaces;
using ELibrary.Orders.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            if (order is null)
            {
                return false;
            }

            await _context.Orders.AddAsync(order);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();

            return orders;
        }
    }
}
