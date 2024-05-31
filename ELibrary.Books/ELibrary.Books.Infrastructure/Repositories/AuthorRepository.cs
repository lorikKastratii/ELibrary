using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace ELibrary.Books.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(AppDbContext context, ILogger<AuthorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Author> AddAsync(Author author)
        {
            try
            {
                await _context.Authors.AddAsync(author);
                var result = await _context.SaveChangesAsync();

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

        public async Task<Author> GetAuthorById(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author is null)
            {
                _logger.LogWarning($"Author with Id: {id} was not found");
            }

            return author;
        }

        public Task<List<Author>> GetAuthors()
        {
            throw new NotImplementedException();
        }
    }
}
