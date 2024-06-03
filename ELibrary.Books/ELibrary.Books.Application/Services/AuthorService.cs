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

        public async Task<ServiceResponse<AuthorDto>> CreateAuthorAsync(AuthorDto authorDto)
        {
            if (authorDto is null)
            {
                _logger.LogError("Author cannot be null.");
                return new ServiceResponse<AuthorDto>(AuthorErrors.AUTHOR_EMPTY);
            }

            var author = _mapper.Map<Author>(authorDto);

            var response = await _authorRepository.CreateAsync(author);

            if (response is null)
            {
                return new ServiceResponse<AuthorDto>(AuthorErrors.AUTHOR_CREATION_ERROR);
            }

            //TODO: change this to dto
            return new ServiceResponse<AuthorDto>(_mapper.Map<AuthorDto>(author));
        }

        public async Task<ServiceResponse<AuthorDto>> GetAuthorByIdAsync(int id)
        {
            if (id < 0)
            {
                return new ServiceResponse<AuthorDto>(AuthorErrors.AUTHOR_EMPTY);
            }

            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author is null)
            {
                _logger.LogError($"Author with {id} does not exists.");
                return new ServiceResponse<AuthorDto>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            return new ServiceResponse<AuthorDto>(_mapper.Map<AuthorDto>(author));
        }

        public async Task<ServiceResponse<List<AuthorDto>>> GetAuthorsAsync()
        {
            var authors = await _authorRepository.GetAuthorsAsync();

            if (authors is null || authors.Count < 0)
            {
                return new ServiceResponse<List<AuthorDto>>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            var authorDtos = _mapper.Map<List<AuthorDto>>(authors);

            return new ServiceResponse<List<AuthorDto>>(authorDtos);
        }
    }
}
