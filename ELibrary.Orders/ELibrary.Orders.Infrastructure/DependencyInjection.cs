using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Domain.Interfaces;
using ELibrary.Orders.Infrastructure.Clients;
using ELibrary.Orders.Infrastructure.Data;
using ELibrary.Orders.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Orders.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastuctureModule(this IServiceCollection services, string connectionString)
        {
            //todo: move these to separate classes
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserClient, UserClient>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
