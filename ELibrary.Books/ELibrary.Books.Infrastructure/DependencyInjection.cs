using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Clients;
using ELibrary.Books.Infrastructure.Data;
using ELibrary.Books.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ELibrary.Books.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IElasticClient>(sp =>
            {
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("books")
                    .BasicAuthentication("elastic", "12345");
                    //.ServerCertificateValidationCallback((sender, certificate, chain, errors) => true);

                return new ElasticClient(settings);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();

            var connectionString = configuration.GetConnectionString("LocalConnection");
            //var connectionString = "Server=LORIK\\SQLEXPRESS;Database=ELibraryBooks;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
