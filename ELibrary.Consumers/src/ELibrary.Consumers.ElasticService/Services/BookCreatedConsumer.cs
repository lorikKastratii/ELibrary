using ELibrary.Consumers.ElasticService.Clients;
using ELibrary.Consumers.ElasticService.Interfaces;
using ELibrary.Contracts.Events;
using MassTransit;

namespace ELibrary.Consumers.Book.Services
{
    public class BookCreatedConsumer : IConsumer<BookCreated>
    {
        private readonly ILogger<BookCreatedConsumer> _logger;
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IBookClient _bookClient;

        public BookCreatedConsumer(ILogger<BookCreatedConsumer> logger, IElasticSearchService elasticSearchService, IBookClient bookClient)
        {
            _logger = logger;
            _elasticSearchService = elasticSearchService;
            _bookClient = bookClient;
        }

        public async Task Consume(ConsumeContext<BookCreated> context)
        {
            var bookId = context.Message.Id;
            _logger.LogInformation("Received BookCreated event on ElasticService Consumer with BookId: {BookId}", bookId);

            var book = await _bookClient.GetBookAsync(bookId);

            try
            {
                var response = await _elasticSearchService.IndexAsync(book, "books");

                if (response is false)
                {
                    _logger.LogError("Failed to index book on Elastic with BookId: {BookId}", bookId);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to index book on Elastic with BookId: {BookId} with Exception: {ex}", bookId, ex.Message);
            }

            _logger.LogInformation("Successully update elastic for book with Id: {BookId}", bookId);
        }
    }
}
