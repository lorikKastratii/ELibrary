using ELibrary.Orders.Infrastructure.ExternalServices;

namespace ELibrary.Orders.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
    }
}
