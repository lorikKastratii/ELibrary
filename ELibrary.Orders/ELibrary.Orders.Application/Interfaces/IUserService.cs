using ELibrary.Orders.Domain.Entity;

namespace ELibrary.Orders.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
    }
}
