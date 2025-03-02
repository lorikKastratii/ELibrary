using ELibrary.Consumers.Book.Services;
using MassTransit;

namespace ELibrary.Consumers.Book
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
                    // This automatically creates the receive endpoint for the consumer
                    //cfg.ConfigureEndpoints(context);

                    cfg.ReceiveEndpoint("book-created-book-consumer", e =>
                    {
                        e.ConfigureConsumer<BookCreatedConsumer>(context);
                        e.Bind("BookCreated");  // <-- Make sure it binds to the correct exchange
                    });
                });
            });
            return builder;
        }
    }
}
