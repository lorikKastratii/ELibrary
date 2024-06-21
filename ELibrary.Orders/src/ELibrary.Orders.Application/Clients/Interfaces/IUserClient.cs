using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Application.Clients.Interfaces
{
    public interface IUserClient
    {
        Task<User> GetUserById(int id);
    }
}
