using ELibrary.Users.Domain.Entities;

namespace ELibrary.Users.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
