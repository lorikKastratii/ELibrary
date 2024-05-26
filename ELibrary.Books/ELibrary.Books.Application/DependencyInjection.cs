using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Books.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
