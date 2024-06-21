using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ELibrary.Orders.Infrastructure.Clients
{
    public class UserClient : IUserClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserClient> _logger;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;

        public UserClient(
            IHttpClientFactory httpClientFactory,
            ILogger<UserClient> logger,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _baseUrl = configuration["UserService:BaseUrl"] ?? "https://localhost:44316/";
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<User> GetUserById(int id)
        {
            var user = new User();

            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var url = $"{_baseUrl}api/user/getuserbyid/{id}";

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode is false)
                {
                    _logger.LogError("Error: {statusCode}", response.StatusCode);
                    return null;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                user = JsonSerializer.Deserialize<User>(jsonResponse, _options);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deserializing User.");
            }

            return user;
        }
    }
}
