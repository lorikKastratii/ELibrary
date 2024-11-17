using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Domain.Entity;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ELibrary.Orders.Infrastructure.Clients
{
    public class UserClient : IUserClient
    {
        private readonly ILogger<UserClient> _logger;
        private readonly HttpClient _httpClient;

        public UserClient(
            ILogger<UserClient> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var url = $"{_httpClient.BaseAddress}api/user/getuserbyid/{id}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    return user;
                }

                _logger.LogError("Call to UserMS failed with Status Code: {statusCode}", response.StatusCode);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while communicating with Users MS.");
            }

            return null;
        }
    }
}
