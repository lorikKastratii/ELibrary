using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Orders.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
