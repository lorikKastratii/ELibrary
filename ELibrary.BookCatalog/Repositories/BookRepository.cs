using ELibrary.BookCatalog.Database;
using ELibrary.BookCatalog.Entity;

namespace ELibrary.BookCatalog.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public BookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateBook(Book book)
        {
            var result = _dbContext.Books.Add(book);

            _dbContext.SaveChanges();

            return 1;
        }

        public bool DeleteBook(int id)
        {
            var test = _dbContext.Books.Find(id);

            if (test is null)
            {
                return false;
            }

            _dbContext.Books.Remove(test);
            _dbContext.SaveChanges();

            return true;
        }

        public List<Book> GetAllBooks()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetBook(int id)
        {
            var result = _dbContext.Books.Find(id);

            return result;
        }

        public List<Book> Search(string query)
        {
            var books = new List<Book>();

            if (String.IsNullOrEmpty(query) is false)
            {
                books = _dbContext.Books.Where
                    (x => x.Title.Contains(query.ToLower())).ToList();
            }

            return books;
        }

        public bool UpdateBook(Book book)
        {
            var bookToUpdate = _dbContext.Books.Find(book.Id);

            if (bookToUpdate is null)
            {
                return false;
            }

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();

            return true;
        }
    }
}