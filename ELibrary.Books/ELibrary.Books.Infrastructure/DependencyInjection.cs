using ELibrary.Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Books.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            //TODO: move this to appsettings
            var connectionString = "Server=LORIK\\SQLEXPRESS;Database=ELibraryBooks;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
