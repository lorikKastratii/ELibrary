using ELibrary.BookCatalog.Entity;
using ELibrary.BookCatalog.Extensions;

namespace ELibrary.BookCatalog.Services
{
    public interface IBookService
    {
        ServiceResponse<Book> GetBook(int id);
        List<Book> GetAllBooks();
        List<Book> SearchBook(string query);
        ServiceResponse<Book> CreateBook(Book book);
        ServiceResponse<Book> DeleteBook(int id);
        ServiceResponse<Book> UpdateBook(Book book);
    }
}
