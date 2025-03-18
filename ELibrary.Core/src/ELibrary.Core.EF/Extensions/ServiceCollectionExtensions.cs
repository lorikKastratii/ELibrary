using ELibrary.Core.EF.Context;
using ELibrary.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Core.EF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddELibraryEntityFramework<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : AppDbContext
        {
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //testt.
            return services;
        }
    }
}
