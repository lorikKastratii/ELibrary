using Elibrary.Books.Domain.Entity;

namespace Elibrary.Books.Domain.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
        Task<bool> CreateBookAsync(Book book);
    }
}
