using Elibrary.Books.Domain.Entity;

namespace Elibrary.Books.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> CreateAsync(Author author);
        Task<List<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
    }
}
