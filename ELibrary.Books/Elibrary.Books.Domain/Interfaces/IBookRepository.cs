using ELibrary.Books.Domain.Entity;

namespace ELibrary.Books.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken);
        Task<bool> CreateBookAsync(Book book, CancellationToken cancellationToken);
        Task<bool> UpdateBookAsync(Book book, CancellationToken cancellationToken);
        Task<Book> GetBookByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Book>> GetBooksByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<List<Book>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken);
    }
}
