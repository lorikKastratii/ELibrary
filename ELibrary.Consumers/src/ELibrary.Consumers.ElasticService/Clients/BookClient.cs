namespace ELibrary.Consumers.ElasticService.Clients
{
    public class BookClient : IBookClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BookClient> _logger;

        public BookClient(HttpClient httpClient, ILogger<BookClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Models.Book> GetBookAsync(int id)
        {
            var url = $"{_httpClient.BaseAddress}api/book/getbookbyid/{id}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var book = await response.Content.ReadFromJsonAsync<Models.Book>();
                    return book;
                }

                _logger.LogError("Failed to get book with Id: {id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception was thrown during communicating with Book Service.");
            }

            return null;
        }
    }
}
