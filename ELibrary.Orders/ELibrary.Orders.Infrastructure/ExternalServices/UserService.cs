using ELibrary.Orders.Domain.Entity;
using ELibrary.Orders.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ELibrary.Orders.Infrastructure.ExternalServices
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserService> _logger;

        public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = new User();

            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var url = $"https://localhost:44316/api/user/getuserbyid/{id}";

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error: {response.StatusCode}");
                    return null;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                user = JsonSerializer.Deserialize<User>(jsonResponse, options);
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
