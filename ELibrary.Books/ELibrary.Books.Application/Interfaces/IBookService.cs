using ELibrary.Books.Application.Dtos;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IBookService
    {
        List<BookDto> GetBooks();
        Task<bool> CreateBookAsync(BookDto bookDto);
    }
}
