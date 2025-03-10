using ELibrary.Contracts.Events;
using MassTransit;
using System.Net;

namespace ELibrary.Consumers.Book.Services
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
            _logger.LogInformation("Received BookCreated event on Book Consumer with BookId: {BookId}", context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
