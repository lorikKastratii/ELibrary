using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Users.Domain.Extensions
{
    public static class DomainModule
    {
        public static WebApplicationBuilder AddDomainModule(this WebApplicationBuilder builder)
        {
            return builder;
        }
    }
}
