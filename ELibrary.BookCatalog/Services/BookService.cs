using ELibrary.BookCatalog.Entity;
using ELibrary.BookCatalog.Extensions;
using ELibrary.BookCatalog.Repositories;

namespace ELibrary.BookCatalog.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ServiceResponse<Book> CreateBook(Book book)
        {
            if (book is null)
            {
                return new ServiceResponse<Book>(Errors.BOOK_IS_NULL);
            }

            var result = _bookRepository.CreateBook(book);

            if (result is not 1)
            {
                return new ServiceResponse<Book>(Errors.BOOK_NOT_ADDED);
            }

            return new ServiceResponse<Book> { IsSuccess = true };
        }

        public ServiceResponse<Book> DeleteBook(int id)
        {
            var result = _bookRepository.DeleteBook(id);

            if (result is false)
            {
                return new ServiceResponse<Book>(Errors.BOOK_NOT_ADDED);
            }

            return new ServiceResponse<Book> { IsSuccess = true };
        }

        public List<Book> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();

            if (books.Any() is false || books is null)
            {
                return null;
            }

            return books;
        }

        public ServiceResponse<Book> GetBook(int id)
        {
            if (id <= 0)
            {
                return new ServiceResponse<Book>(Errors.BOOK_NOT_FOUND);
            }

            var response = _bookRepository.GetBook(id);

            if (response is null)
            {
                return new ServiceResponse<Book>(Errors.BOOK_NOT_FOUND);
            }

            return new ServiceResponse<Book>
            {
                Data = response
            };
        }

        public List<Book> SearchBook(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            var books = _bookRepository.Search(query);

            return books;
        }

        public ServiceResponse<Book> UpdateBook(Book book)
        {
            if (book is null)
            {
                return new ServiceResponse<Book>(Errors.BOOK_IS_NULL);
            }

            var response = _bookRepository.UpdateBook(book);

            if (response is false)
            {
                return new ServiceResponse<Book>(Errors.BOOK_NOT_ADDED);
            }

            return new ServiceResponse<Book>
            {
                Data = book
            };
        }
    }
}
