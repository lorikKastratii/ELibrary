using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Orders.Infrastructure.Extensions
{
    public static class ClientModuleExtension
    {
        public static IServiceCollection AddClientModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBookMSClient(configuration);
            services.AddUsersMSClient(configuration);

            return services;
        }

        public static IServiceCollection AddBookMSClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IBookClient, BookClient>(client =>
            {
                var uri = configuration["BookService:BaseUrl"] ?? "http://localhost:8083";
                client.BaseAddress = new Uri(uri);
            });

            return services;
        }
        
        public static IServiceCollection AddUsersMSClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IUserClient, UserClient>(client =>
            {
                var uri = configuration["UserService:BaseUrl"] ?? "http://localhost:8081";
                client.BaseAddress = new Uri(uri);
            });

            return services;
        }
    }
}
