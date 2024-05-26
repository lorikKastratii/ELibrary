using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;

namespace ELibrary.Books.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetBooks()
        {
            var books = _context.Books.ToList();

            return books;
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            var result = await _context.SaveChangesAsync();

            return result == 1;
        }
    }
}
