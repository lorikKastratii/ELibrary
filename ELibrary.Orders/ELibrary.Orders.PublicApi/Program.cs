using ELibrary.Orders.Infrastructure;
using ELibrary.Orders.Application;
using ELibrary.Orders.PublicApi.Extensions;
using ELibrary.Orders.Domain;

namespace ELibrary.Orders.PublicApi
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

            builder.AddJwtAuthentication();

            builder.Services
                .AddDomainModule()
                .AddInfrastuctureModule()
                .AddApplicationModule();

            builder.Services.AddHttpClient();

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
