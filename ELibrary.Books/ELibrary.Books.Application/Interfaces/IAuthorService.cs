using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Extensions;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<ServiceResponse<Author>> GetAuthorByIdAsync(int id);
        Task<ServiceResponse<List<Author>>> GetAuthorsAsync();
        Task<ServiceResponse<Author>> AddAuthorAsync(AuthorDto author);
    }
}
