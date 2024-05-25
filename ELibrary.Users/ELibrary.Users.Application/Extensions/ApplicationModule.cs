using ELibrary.Users.Application.Interfaces;
using ELibrary.Users.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Users.Application.Extensions
{
    public static class ApplicationModule
    {
        public static WebApplicationBuilder AddApplicationModule(this WebApplicationBuilder builder)
        {
            AddDependencyInjections(builder.Services);

            return builder;
        }

        private static void AddDependencyInjections(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
