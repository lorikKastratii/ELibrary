using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using ELibrary.Books.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Books.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            //TODO: move this to appsettings
            var connectionString = "Server=DESKTOP-BRGQULH;Database=ELibraryBooks;Trusted_Connection=True;TrustServerCertificate=True";
            //var connectionString = "Data Source= DESKTOP-BRGQULH; Integrated Security=true;Initial Catalog= eTravelCompany;"
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
