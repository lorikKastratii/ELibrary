using Elibrary.Books.Domain.Entity;

namespace ELibrary.Books.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorById(int id);
    }
}
