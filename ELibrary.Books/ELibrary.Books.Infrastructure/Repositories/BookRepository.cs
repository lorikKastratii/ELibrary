using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Domain.Exceptions.Book;
using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BookRepository> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BookRepository(AppDbContext context, ILogger<BookRepository> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken)
        {
            var books = await _context.Books
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return books;
        }

        public async Task<bool> CreateBookAsync(Book book, CancellationToken cancellationToken)
        {
            await _context.Books.AddAsync(book, cancellationToken);

            try
            {
                var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating of Book failed.");
                return false;
            }
        }

        public async Task<bool> UpdateBookAsync(Book book, CancellationToken cancellationToken)
        {
            var bookEntity = await _context.Books.FindAsync(book.Id, cancellationToken);

            if (bookEntity is null)
            {
                _logger.LogError("Failed to update book. Book with Id: {id} does not exist.", book.Id);
                return false;
            }

            _context.Entry(bookEntity).CurrentValues.SetValues(book);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result == 1;
        }

        public async Task<Book> GetBookByIdAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id, cancellationToken);

            if (book is null)
            {
                throw new BookNotFoundException(id);
            }

            return book;
        }

        public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var books = await _context.Books
                .Where(x => x.BookCategories.Any(bc => bc.CategoryId == categoryId))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return books;
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken)
        {
            var books = await _context.Books
                .Where(x => x.AuthorId == authorId)
                .ToListAsync(cancellationToken);

            return books;
        }
    }
}
