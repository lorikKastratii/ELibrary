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
                _logger.LogError("Cannot create book because Author with Id: {@id} does not exists", bookDto.AuthorId);
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

        public async Task<bool> UpdateBookAsync(BookDto bookDto)
        {
            if (bookDto is null)
            {
                _logger.LogWarning("Book cannot be null");
                return false;
            }

            var book = _mapper.Map<Book>(bookDto);

            var response = await _bookRepository.UpdateBookAsync(book);

            if (response is false)
            {
                _logger.LogWarning("Failed to update book with Id: {id}", bookDto.Id);
            }

            return response;
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid Id: {id}. Id cannot be 0", id);
                return null;
            }

            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book is null)
            {
                _logger.LogWarning("Book with Id: {id} not found", id);
                return null;
            }

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;
        }

        public async Task<List<BookDto>> GetBooksByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                _logger.LogError("Category Id: {id} cannot be negative.", categoryId);

                return null;
            }

            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);

            if (books is null || books.Any() is false)
            {
                _logger.LogError("No books exists for CategoryId: {id}.", categoryId);
            }

            var booksDto = _mapper.Map<List<BookDto>>(books);

            return booksDto;
        }
    }
}
