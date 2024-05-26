using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Books.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
