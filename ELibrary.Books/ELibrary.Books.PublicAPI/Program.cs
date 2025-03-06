using ELibrary.Books.Domain;
using ELibrary.Books.Application;
using Serilog;
using ELibrary.Books.Infrastructure.Extensions;

namespace ELibrary.Books.PublicAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((context, configuration) =>
            {
                var elasticUrl = builder.Configuration["ElasticClientSettings:ElasticUrl"] ?? "http://elasticsearch:9200";
                var elasticUser = builder.Configuration["ElasticClientSettings:ElasticUser"] ?? "elastic";
                var elasticPassword = builder.Configuration["ElasticClientSettings:ElasticPassword"] ?? "12345";

                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.WithProperty("@timestamp", DateTime.UtcNow)
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(elasticUrl))
                    {
                        ModifyConnectionSettings = conn => conn.BasicAuthentication(elasticUser, elasticPassword),
                        AutoRegisterTemplate = true,
                        OverwriteTemplate = true,
                        IndexFormat = "elibrary-logging-{0:yyyy.MM.dd}",
                        AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv7
                    });
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services
                .AddDomainModule()
                .AddApplicationModule()
                .AddInfrastructureModule(builder.Configuration)
                .AddMassTransitWithRabbitMq(builder.Configuration);

            builder.AddElasticClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseSerilogRequestLogging();

            app.MapControllers();

            app.Run();
        }
    }
}
