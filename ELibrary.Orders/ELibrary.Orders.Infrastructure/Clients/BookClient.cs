using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Application.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ELibrary.Orders.Infrastructure.Clients
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

        public async Task<Book> GetBookAsync(int id)
        {
            var url = $"{_httpClient.BaseAddress}book/getbookbyid{id}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var book = await response.Content.ReadFromJsonAsync<Book>();
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
