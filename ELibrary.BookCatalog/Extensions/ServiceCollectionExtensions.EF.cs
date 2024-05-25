using ELibrary.BookCatalog.Database;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.BookCatalog.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddEntityFramework(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("LocalConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return builder;
        }
    }
}
