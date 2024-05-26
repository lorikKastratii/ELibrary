using Microsoft.Extensions.DependencyInjection;

namespace Elibrary.Books.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainModule (this IServiceCollection services)
        {
            return services;
        }
    }
}
