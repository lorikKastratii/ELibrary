using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ELibrary.Orders.Infrastructure.Clients
{
    public class UserClient : IUserClient
    {
        private readonly ILogger<UserClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClient(
            ILogger<UserClient> logger, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var url = $"{_httpClient.BaseAddress}api/user/getuserbyid/{id}";

            _logger.LogInformation($"url is: {url}");

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext is null.");
                return null;
            }

            var token = httpContext.Request.Headers.Authorization.ToString();
            _logger.LogInformation($"token is: {token}");

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Authorization token is missing.");
                return null;
            }

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

                var response = await _httpClient.SendAsync(requestMessage);

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
