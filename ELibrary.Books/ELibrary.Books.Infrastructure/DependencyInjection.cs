using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using ELibrary.Books.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Books.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            var connectionString = configuration.GetConnectionString("LocalConnection");
            //var connectionString = "Server=LORIK\\SQLEXPRESS;Database=ELibraryBooks;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
