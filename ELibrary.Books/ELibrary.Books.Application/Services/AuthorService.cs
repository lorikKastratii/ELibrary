using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Interfaces;

namespace ELibrary.Books.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> GetAuthorById(int id)
        {
            if (id < 0)
            {
                return null;
            }

            var author = await _authorRepository.GetAuthorById(id);

            return author;
        }
    }
}
