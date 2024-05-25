using ELibrary.Orders.Infrastructure.Interfaces;
using System.Text.Json;

namespace ELibrary.Orders.Infrastructure.ExternalServices
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User> GetUserByEmail(string username)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var encodedUsername = Uri.EscapeDataString(username);

            var url = $"https://localhost:44316/api/user/getuserbyusername/{encodedUsername}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Makes deserialization case-insensitive
            };

            var user = new User();

            try
            {
                user = JsonSerializer.Deserialize<User>(jsonResponse, options);
                return user;
            }
            catch (JsonException ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return null;
            }

            return user;
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
