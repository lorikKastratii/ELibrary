using ELibrary.Users.Domain.Entities;
using ELibrary.Users.Domain.Interfaces;
using ELibrary.Users.Infrastructure.Data;
using ELibrary.Users.Infrastructure.Repositories;
using ELibrary.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Users.Infrastructure.Extensions
{
    public static class InfrastructureModule
    {
        public static WebApplicationBuilder AddInfrastructureModule(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
            var services = builder.Services;

            Console.WriteLine($"-------------------> This is connection string:  {connectionString}");
            AddDependencyInjections(services);
            AddEntityFramework(services, connectionString);

            return builder;
        }

        private static void AddDependencyInjections(IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddEntityFramework(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
