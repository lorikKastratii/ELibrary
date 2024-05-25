using ELibrary.BookCatalog.Entity;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.BookCatalog.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
