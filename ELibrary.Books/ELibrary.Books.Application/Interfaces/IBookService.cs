using ELibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Extensions;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IBookService
    {
        Task<ServiceResponse<List<BookDto>>> GetBooksAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<BookDto>> CreateBookAsync(BookDto bookDto, CancellationToken cancellationToken);
        Task<ServiceResponse<bool>> UpdateBookAsync(BookDto bookDto, CancellationToken cancellationToken);
        Task<ServiceResponse<BookDto>> GetBookByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResponse<List<BookDto>>> GetBooksByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<ServiceResponse<List<BookDto>>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken);
    }
}
