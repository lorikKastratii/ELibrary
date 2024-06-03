using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Author> CreateAsync(Author author)
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

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return author;
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();

            return authors;
        }
    }
}
