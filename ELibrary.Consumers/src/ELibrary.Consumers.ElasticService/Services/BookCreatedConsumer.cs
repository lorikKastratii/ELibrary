using ELibrary.Contracts.Events;
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
            return Task.CompletedTask;
        }
    }
}
