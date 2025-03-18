using Microsoft.EntityFrameworkCore;

namespace ELibrary.Core.EF.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
    }
}
