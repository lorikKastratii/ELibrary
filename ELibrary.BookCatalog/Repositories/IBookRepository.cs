using ELibrary.BookCatalog.Entity;

namespace ELibrary.BookCatalog.Repositories
{
    public interface IBookRepository
    {
        public Book GetBook(int id);
        public List<Book> GetAllBooks();
        int CreateBook(Book book);
        bool DeleteBook(int id);
        bool UpdateBook(Book book);
        List<Book> Search(string query);
    }
}
