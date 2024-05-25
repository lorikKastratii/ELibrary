using ELibrary.Orders.Domain.Interfaces;
using ELibrary.Orders.Infrastructure.Data;
using ELibrary.Orders.Infrastructure.ExternalServices;
using ELibrary.Orders.Infrastructure.Interfaces;
using ELibrary.Orders.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Orders.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastuctureModule(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserService, UserService>();

            var connectionString = "Server=LORIK\\SQLEXPRESS;Database=ELibraryOrders;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
