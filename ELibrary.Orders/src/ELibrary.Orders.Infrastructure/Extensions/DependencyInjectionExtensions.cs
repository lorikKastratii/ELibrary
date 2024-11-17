using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Domain.Interfaces;
using ELibrary.Orders.Infrastructure.Clients;
using ELibrary.Orders.Infrastructure.Data;
using ELibrary.Orders.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Orders.Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkConfig(configuration);

            //injections
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserClient, UserClient>();

            return services;
        }

        public static IServiceCollection AddEntityFrameworkConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LocalConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
