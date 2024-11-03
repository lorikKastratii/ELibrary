using ELibrary.Books.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Books.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainModule (this IServiceCollection services)
        {
            return services;
        }
    }
}
