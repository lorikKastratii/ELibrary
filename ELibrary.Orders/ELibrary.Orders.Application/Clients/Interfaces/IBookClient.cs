using ELibrary.Orders.Application.Models;

namespace ELibrary.Orders.Application.Clients.Interfaces
{
    public interface IBookClient
    {
        Task<Book> GetBookAsync(int id);
    }
}