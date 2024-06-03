using Elibrary.Books.Domain.Entity;

namespace Elibrary.Books.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync();
        Task<bool> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int id);
        Task<List<Book>> GetBooksByCategoryAsync(int categoryId);
    }
}
