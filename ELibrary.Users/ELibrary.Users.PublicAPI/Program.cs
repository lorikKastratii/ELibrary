using ELibrary.Users.Infrastructure.Extensions;
using ELibrary.Users.Application.Extensions;
using ELibrary.Users.Domain.Extensions;
using ELibrary.Users.PublicAPI.Extensions;

namespace ELibrary.Users.PublicAPI
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

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder
                .AddDomainModule()
                .AddApplicationModule()
                .AddInfrastructureModule();
                //.AddPublicApiModule();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseForwardedHeaders();
            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
