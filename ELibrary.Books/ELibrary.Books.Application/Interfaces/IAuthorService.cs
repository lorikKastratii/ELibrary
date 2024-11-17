using ELibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Extensions;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<ServiceResponse<AuthorDto>> GetAuthorByIdAsync(int id);
        Task<ServiceResponse<List<AuthorDto>>> GetAuthorsAsync();
        Task<ServiceResponse<AuthorDto>> CreateAuthorAsync(AuthorDto author);
        Task<ServiceResponse<bool>> UpdateAuthorAsync(AuthorDto authorDto);
        Task<ServiceResponse<List<BookDto>>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken);
    }
}
