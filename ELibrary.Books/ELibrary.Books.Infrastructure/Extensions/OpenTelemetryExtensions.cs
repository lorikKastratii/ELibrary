using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ELibrary.Books.Infrastructure.Extensions
{
    public static class OpenTelemetryExtensions
    {
        public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ELibrary"))
                        .AddAspNetCoreInstrumentation()
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
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ELibrary"))
                        .AddAspNetCoreInstrumentation()
                        .AddOtlpExporter(opt =>
                        {
                            opt.Endpoint = new Uri("http://otel-collector:4317");
                        });
                });

            return builder;
        }
    }
}
