using ELibrary.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Users.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int,
    UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().Ignore(c => c.AccessFailedCount)
                                               .Ignore(c => c.LockoutEnabled)
                                               .Ignore(c => c.LockoutEnd)
                                               .Ignore(c => c.SecurityStamp)
                                               .Ignore(c => c.ConcurrencyStamp)
                                               .Ignore(c => c.AccessFailedCount);

            builder.Entity<User>(entity => entity.ToTable("Users"));
            builder.Entity<Role>(entity => entity.ToTable("Roles"));
            builder.Entity<UserRole>(entity => entity.ToTable("UserRoles"));
            builder.Entity<UserClaim>(entity => entity.ToTable("UserClaims"));
            builder.Entity<UserLogin>(entity => entity.ToTable("UserLogins"));
            builder.Entity<RoleClaim>(entity => entity.ToTable("RoleClaims"));
            builder.Entity<UserToken>(entity => entity.ToTable("UserTokens"));            

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.SetSchema(null);
            }
        }
    }
}
