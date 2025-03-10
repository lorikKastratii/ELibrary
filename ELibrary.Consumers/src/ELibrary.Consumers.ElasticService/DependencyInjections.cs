using ELibrary.Consumers.Book.Services;
using ELibrary.Consumers.ElasticService.Clients;
using ELibrary.Consumers.ElasticService.Interfaces;
using ELibrary.Consumers.ElasticService.Services;
using MassTransit;
using Nest;

namespace ELibrary.Consumers.ElasticService
{
    public static class DependencyInjections
    {
        public static WebApplicationBuilder AddConsumerModule(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<BookCreatedConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqHost = builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq";
                    cfg.Host(rabbitMqHost, "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("book-created-elastic-consumer", e =>
                    {
                        e.ConfigureConsumer<BookCreatedConsumer>(context);
                        e.Bind("BookCreated");
                    });
                });
            });

            builder.Services.AddSingleton<IElasticClient>(sp =>
            {

                var user = builder.Configuration["ElasticClientSettings:ElasticUser"] ?? "elastic";
                var password = builder.Configuration["ElasticClientSettings:ElasticPassword"] ?? "12345";
                var uri = builder.Configuration["ElasticClientSettings:ElasticUrl"] ?? "http://localhost:9200";

                var settings = new ConnectionSettings(new Uri(uri))
                    .DefaultIndex("books")
                    .BasicAuthentication(user, password)
                    .ServerCertificateValidationCallback((sender, certificate, chain, errors) => true);

                return new ElasticClient(settings);
            });

            builder.Services.AddScoped<IElasticSearchService, ElasticSearchService>();

            builder.Services.AddHttpClient<IBookClient, BookClient>(client =>
            {
                var uri = builder.Configuration["BookService:BaseUrl"] ?? "http://localhost:8083";
                client.BaseAddress = new Uri(uri);
            });

            return builder;
        }
    }
}
