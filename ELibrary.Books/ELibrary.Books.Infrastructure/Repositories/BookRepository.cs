using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(AppDbContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var books = await _context.Books.
                AsNoTracking().
                ToListAsync();

            return books;
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);

            try
            {
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating of Book failed.");
                return false;
            }
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var bookEntity = await _context.Books.FindAsync(book.Id);

            if (bookEntity is null)
            {
                //TODO: add log here
                return false;
            }

            _context.Entry(bookEntity).CurrentValues.SetValues(book);

            var result = await _context.SaveChangesAsync();

            return result == 1;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            return book;
        }
    }
}
