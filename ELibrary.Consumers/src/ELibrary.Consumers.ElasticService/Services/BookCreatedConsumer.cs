using ELibrary.Consumers.ElasticService.Events;
using MassTransit;

namespace ELibrary.Consumers.ElasticService.Services
{
    public class BookCreatedConsumer : IConsumer<BookCreated>
    {
        private readonly ILogger<BookCreatedConsumer> _logger;
        public BookCreatedConsumer(ILogger<BookCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BookCreated> context)
        {
            _logger.LogInformation("Received BookCreated event with BookId: {BookId}", context.Message.Id);
            // You can add additional logic here (e.g., updating a database, calling another service, etc.)
            return Task.CompletedTask;
        }
    }
}
