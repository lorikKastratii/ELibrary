using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Extensions.Errors;
using ELibrary.Books.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(
            IAuthorRepository authorRepository,
            IMapper mapper,
            ILogger<AuthorService> logger,
            IBookService bookService)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
            _bookService = bookService;
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

            if (authors is null || authors.Count == 0)
            {
                _logger.LogWarning("No authors found.");
                return new ServiceResponse<List<AuthorDto>>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            var authorDtos = _mapper.Map<List<AuthorDto>>(authors);

            return new ServiceResponse<List<AuthorDto>>(authorDtos);
        }

        public async Task<ServiceResponse<bool>> UpdateAuthorAsync(AuthorDto authorDto)
        {
            if (authorDto is null)
            {
                _logger.LogError("Cannot update author since author is null");
                return new ServiceResponse<bool>(AuthorErrors.AUTHOR_EMPTY);
            }

            var author = _mapper.Map<Author>(authorDto);

            var response = await _authorRepository.UpdateAuthorAsync(author);

            if (response is false)
            {
                _logger.LogError("Failed to update author with Id: {id}", authorDto.Id);
                return new ServiceResponse<bool>(AuthorErrors.AUTHOR_UPDATING_FAILED);
            }

            return ServiceResponse<bool>.Success();
        }

        public async Task<ServiceResponse<List<BookDto>>> GetBooksByAuthorAsync(int authorId)
        {
            if (authorId <= 0)
            {
                _logger.LogError("Failed to fetch Book for Author. Id cannot be 0.");
                return new ServiceResponse<List<BookDto>>(AuthorErrors.AUTHOR_EMPTY);
            }

            var author = await _authorRepository.GetAuthorByIdAsync(authorId);

            if (author is null)
            {
                _logger.LogError("Author with Id: {id} does not exists.", authorId);
                return new ServiceResponse<List<BookDto>>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            var response = await _bookService.GetBooksByAuthorAsync(authorId);

            if (response.IsSuccess)
            {
                return new ServiceResponse<List<BookDto>>(response.Data);
            }

            return new ServiceResponse<List<BookDto>>(AuthorErrors.AUTHOR_HAS_NO_BOOKS);
        }
    }
}
