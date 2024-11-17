using ELibrary.Books.Domain.Entity;
using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorRepository(AppDbContext context, ILogger<AuthorRepository> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            try
            {
                await _context.Authors.AddAsync(author);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result != 1)
                {
                    _logger.LogError("Failed to save the new author.");
                    return null;
                }

                return author;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating author.");

                return null;
            }
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            return author;
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            var authors = await _context.Authors.
                AsNoTracking().
                ToListAsync();

            return authors;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            var authorEntity = await _context.Authors.FindAsync(author.Id);

            if (authorEntity is null)
            {
                _logger.LogError("Updating Author failed. Author with Id: {id} does not exists.", author.Id);
                return false;
            }

            _context.Entry(authorEntity).CurrentValues.SetValues(author);

            var result = await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }
    }
}
