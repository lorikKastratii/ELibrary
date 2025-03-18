using ELibrary.Core.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Core.EF.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
