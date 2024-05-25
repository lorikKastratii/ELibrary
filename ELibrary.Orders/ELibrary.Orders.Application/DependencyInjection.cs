using ELibrary.Orders.Application.Interfaces;
using ELibrary.Orders.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Orders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
