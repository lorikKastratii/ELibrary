using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Extensions.Errors;
using ELibrary.Books.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger, IAuthorService authorService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
            _authorService = authorService;
        }

        public async Task<ServiceResponse<List<BookDto>>> GetBooksAsync()
        {
            var books = await _bookRepository.GetBooksAsync();

            if (books is null || books.Any() is false)
            {
                return new ServiceResponse<List<BookDto>>(BookErrors.BOOK_EMPTY);
            }

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return new ServiceResponse<List<BookDto>> { Data = bookDtos };
        }

        public async Task<ServiceResponse<BookDto>> CreateBookAsync(BookDto bookDto)
        {
            if (bookDto is null)
            {
                return new ServiceResponse<BookDto>(BookErrors.BOOK_EMPTY);
            }

            var author = await _authorService.GetAuthorByIdAsync(bookDto.AuthorId);

            if (author.Data is null)
            {
                //_logger.LogError($"Cannot create book because Author with Id: {id} does not exists", bookDto.AuthorId);
                return new ServiceResponse<BookDto>(AuthorErrors.AUTHOR_NOT_FOUND);
            }

            var book = _mapper.Map<Book>(bookDto);

            var result = await _bookRepository.CreateBookAsync(book);

            if (result is false)
            {
                return new ServiceResponse<BookDto>(BookErrors.BOOK_CREATION_ERROR);
            }

            return new ServiceResponse<BookDto>(bookDto);
        }
    }
}
