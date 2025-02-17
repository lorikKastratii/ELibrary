using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Clients;
using ELibrary.Books.Infrastructure.Data;
using ELibrary.Books.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RabbitMQ.Client;

namespace ELibrary.Books.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IElasticClient>(sp =>
            {
                //var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                var settings = new ConnectionSettings(new Uri("http://elasticsearch:9200"))
                    .DefaultIndex("books")
                    //.BasicAuthentication("elastic", "12345")
                    .ServerCertificateValidationCallback((sender, certificate, chain, errors) => true);

                return new ElasticClient(settings);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();

            var connectionString = configuration.GetConnectionString("LocalConnection");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator>? configureConsumers = null)
        {
            services.AddMassTransit(x =>
            {
                // If the caller wants to add consumers, run the configuration callback.
                configureConsumers?.Invoke(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    // Use the configuration value "RabbitMQ:Host" if provided, otherwise default to "rabbitmq".
                    var rabbitMqHost = configuration["RabbitMQ:Host"] ?? "rabbitmq";
                    cfg.Host(rabbitMqHost, "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });
            return services;
        }
    }
}
