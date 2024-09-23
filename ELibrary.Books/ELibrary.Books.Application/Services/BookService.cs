using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Extensions.Errors;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Domain.Exceptions.Book;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
            _authorRepository = authorRepository;
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

            var author = await _authorRepository.GetAuthorByIdAsync(bookDto.AuthorId);

            if (author is null)
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

        public async Task<ServiceResponse<BookDto>> GetBookByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid Id: {id}. Id cannot be 0", id);
                return ServiceResponse<BookDto>.Failure("The id provided is invalid");
            }
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id, cancellationToken);
                var bookDto = _mapper.Map<BookDto>(book);

                return ServiceResponse<BookDto>.Success(bookDto);
            }
            catch(BookNotFoundException ex)
            {
                _logger.LogInformation($"Book with Id: {id} does not exists", id);
                return ServiceResponse<BookDto>.Failure(BookErrors.BOOK_NOT_FOUND);
            }
            
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

        public async Task<ServiceResponse<List<BookDto>>> GetBooksByAuthorAsync(int authorId)
        {
            if (authorId <= 0)
            {
                _logger.LogError("Cannot fetch Books for Author. Id cannot be 0.");
                return null;
            }

            var books = await _bookRepository.GetBooksByAuthorAsync(authorId);

            if (books is null || books.Count == 0)
            {
                _logger.LogWarning("There are no books for Author with Id: {id}", authorId);
                return new ServiceResponse<List<BookDto>>(BookErrors.AUTHOR_BOOKS_EMPTY);
            }

            var bookList = _mapper.Map<List<BookDto>>(books);

            return new ServiceResponse<List<BookDto>>(bookList);
        }
    }
}
