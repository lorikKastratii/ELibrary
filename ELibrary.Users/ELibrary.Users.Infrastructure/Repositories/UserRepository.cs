using ELibrary.Users.Domain.Entities;
using ELibrary.Users.Domain.Interfaces;
using ELibrary.Users.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
