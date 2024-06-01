using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Extensions.Errors;
using ELibrary.Books.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(
            IAuthorRepository authorRepository,
            IMapper mapper,
            ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<Author>> AddAuthorAsync(AuthorDto authorDto)
        {
            if (authorDto is null)
            {
                return new ServiceResponse<Author>(AuthorErrors.AUTHOR_EMPTY);
            }

            var author = _mapper.Map<Author>(authorDto);

            var response = await _authorRepository.AddAsync(author);

            if (response is null)
            {
                return new ServiceResponse<Author>(AuthorErrors.AUTHOR_CREATION_ERROR);
            }

            //TODO: change this to dto
            return new ServiceResponse<Author>(author);
        }

        public async Task<ServiceResponse<Author>> GetAuthorByIdAsync(int id)
        {
            if (id < 0)
            {
                return new ServiceResponse<Author>(AuthorErrors.AUTHOR_EMPTY);
            }

            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author is null)
            {
                return new ServiceResponse<Author>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            return new ServiceResponse<Author>(author);
        }

        public async Task<ServiceResponse<List<Author>>> GetAuthorsAsync()
        {
            var authors = await _authorRepository.GetAuthorsAsync();

            if (authors is null || authors.Count > 0)
            {
                return new ServiceResponse<List<Author>>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            return new ServiceResponse<List<Author>>(authors);
        }
    }
}
