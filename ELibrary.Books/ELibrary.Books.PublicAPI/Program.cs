using ELibrary.Books.Domain;
using ELibrary.Books.Application;
using Serilog;
using ELibrary.Books.Infrastructure.Extensions;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ELibrary.Books.PublicAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ELibrary.Books"))
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri("http://otel-collector:4317");
                    });
            })
            .WithMetrics(metricsProviderBuilder =>
            {
                metricsProviderBuilder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ELibrary.Books"))
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddPrometheusExporter();
            });

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
            //builder.AddOpenTelemetry();
            builder.WebHost.UseUrls("http://+:8083");

            var app = builder.Build();

            app.UseOpenTelemetryPrometheusScrapingEndpoint();
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
