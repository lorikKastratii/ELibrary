using ELibrary.Users.Domain.Entities;

namespace ELibrary.Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
