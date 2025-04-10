using ELibrary.Orders.Application;
using ELibrary.Orders.PublicApi.Extensions;
using ELibrary.Orders.Domain;
using Serilog;
using ELibrary.Orders.Infrastructure.Services;
using ELibrary.Orders.PublicApi.Validators;
using FluentValidation;
using ELibrary.Orders.Application.Requests;
using ELibrary.Orders.Infrastructure.Extensions;

namespace ELibrary.Orders.PublicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //serilog
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.AddJwtAuthentication();

            builder.Services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();

            builder.Services
                .AddDomainModule()
                .AddInfrastructureModule(builder.Configuration)
                .AddApplicationModule();

            // move this to extensions methods
            builder.Services.AddHttpClient();
            //builder.Services.AddHttpClient<IBookClient, BookClient>(client =>
            //{
            //    var uri = builder.Configuration["BookService:BaseUrl"];
            //    client.BaseAddress = new Uri(uri);
            //});

            builder.Services.AddSingleton<RabbitMQClientService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
