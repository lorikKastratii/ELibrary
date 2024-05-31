using ELibrary.Books.Application.Dtos;
using ELibrary.Books.Application.Extensions;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IBookService
    {
        Task<ServiceResponse<List<BookDto>>> GetBooksAsync();
        Task<ServiceResponse<BookDto>> CreateBookAsync(BookDto bookDto);
    }
}
