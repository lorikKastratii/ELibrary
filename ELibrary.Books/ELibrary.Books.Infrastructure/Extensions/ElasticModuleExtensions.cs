using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ELibrary.Books.Infrastructure.Extensions
{
    public static class ElasticModuleExtensions
    {
        public static WebApplicationBuilder AddElasticClient (this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IElasticClient>(sp =>
            {
                var user = builder.Configuration["ElasticClientSettings:ElasticUser"] ?? "elastic";
                var password = builder.Configuration["ElasticClientSettings:ElasticPassword"] ?? "12345";
                var uri = builder.Configuration["ElasticClientSettings:ElasticUrl"] ?? "http://localhost:9200";

                var settings = new ConnectionSettings(new Uri(uri))
                    .DefaultIndex("books")
                    .BasicAuthentication(user, password)
                    .ServerCertificateValidationCallback((sender, certificate, chain, errors) => true);

                return new ElasticClient(settings);
            });

            return builder;
        }
    }
}
