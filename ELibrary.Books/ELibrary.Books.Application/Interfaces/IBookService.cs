using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Extensions;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IBookService
    {
        Task<ServiceResponse<List<BookDto>>> GetBooksAsync();
        Task<ServiceResponse<BookDto>> CreateBookAsync(BookDto bookDto);
        Task<bool> UpdateBookAsync(BookDto bookDto);
        Task<BookDto> GetBookByIdAsync(int id);
        Task<List<BookDto>> GetBooksByCategoryAsync(int categoryId);
        Task<ServiceResponse<List<BookDto>>> GetBooksByAuthorAsync(int authorId);
    }
}
