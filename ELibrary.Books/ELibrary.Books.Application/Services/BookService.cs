﻿using AutoMapper;
using ELibrary.Books.Domain.Entity;
using ELibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Extensions.Errors;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Domain.Exceptions.Book;
//using Microsoft.Extensions.Logging;
using MassTransit;
using ELibrary.Contracts.Events;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IBus _bus;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger,
            IAuthorRepository authorRepository, IElasticSearchService elasticSearchService, IBus bus)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
            _authorRepository = authorRepository;
            _elasticSearchService = elasticSearchService;
            _bus = bus;
        }

        public async Task<ServiceResponse<List<BookDto>>> GetBooksAsync(CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBooksAsync(cancellationToken);

            if (books is null || books.Any() is false)
            {
                return new ServiceResponse<List<BookDto>>(BookErrors.BOOK_EMPTY);
            }

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return new ServiceResponse<List<BookDto>> { Data = bookDtos };
        }

        public async Task<ServiceResponse<BookDto>> CreateBookAsync(BookDto bookDto, CancellationToken cancellationToken)
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

            var result = await _bookRepository.CreateBookAsync(book, cancellationToken);

            if (result is false)
            {
                return new ServiceResponse<BookDto>(BookErrors.BOOK_CREATION_ERROR);
            }

            try
            {
                //await _elasticSearchService.IndexAsync(book, "books");
                await _bus.Publish(new BookCreated { Id = book.Id }, cancellationToken);
                _logger.LogInformation("BookCreated event published for Book Id: {BookId}", book.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish BookCreated event for Book Id: {BookId}", book.Id);
                return new ServiceResponse<BookDto>(BookErrors.BOOK_CREATION_ERROR);
            }

            return new ServiceResponse<BookDto>(bookDto);
        }

        public async Task<ServiceResponse<bool>> UpdateBookAsync(BookDto bookDto, CancellationToken cancellationToken)
        {
            if (bookDto is null)
            {
                _logger.LogWarning("Book cannot be null");
                return ServiceResponse<bool>.Failure(BookErrors.BOOK_EMPTY);
            }

            var book = _mapper.Map<Book>(bookDto);

            var response = await _bookRepository.UpdateBookAsync(book, cancellationToken);

            if (response is false)
            {
                _logger.LogWarning("Failed to update book with Id: {id}", bookDto.Id);
                return ServiceResponse<bool>.Failure(BookErrors.BOOK_UPDATING_FAILED);
            }

            return ServiceResponse<bool>.Success(true);
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
            catch (BookNotFoundException ex)
            {
                _logger.LogInformation($"Book with Id: {id} does not exists", id);
                return ServiceResponse<BookDto>.Failure(BookErrors.BOOK_NOT_FOUND);
            }
        }

        public async Task<ServiceResponse<List<BookDto>>> GetBooksByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (categoryId <= 0)
            {
                _logger.LogError("Category Id: {id} cannot be negative.", categoryId);
                return ServiceResponse<List<BookDto>>.Failure(BookErrors.BOOK_NOT_FOUND);
            }

            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId, cancellationToken);

            if (books is null || books.Any() is false)
            {
                _logger.LogError("No books exists for CategoryId: {id}.", categoryId);
                return ServiceResponse<List<BookDto>>.Failure(BookErrors.BOOK_NOT_FOUND);
            }

            var booksDto = _mapper.Map<List<BookDto>>(books);
            return ServiceResponse<List<BookDto>>.Success(booksDto);
        }

        public async Task<ServiceResponse<List<BookDto>>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken)
        {
            if (authorId <= 0)
            {
                _logger.LogError("Cannot fetch Books for Author. Id cannot be 0.");
                return null;
            }

            var books = await _bookRepository.GetBooksByAuthorAsync(authorId, cancellationToken);

            if (books is null || books.Count == 0)
            {
                _logger.LogWarning("There are no books for Author with Id: {id}", authorId);
                return new ServiceResponse<List<BookDto>>(BookErrors.AUTHOR_BOOKS_EMPTY);
            }

            var bookList = _mapper.Map<List<BookDto>>(books);

            return new ServiceResponse<List<BookDto>>(bookList);
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            var response = await _elasticSearchService.SearchAsync<Book>("books", query);
            return response;
        }

        public async Task PopulateElasticWithBooksAsync(CancellationToken cancellationToken = default)
        {
            var books = await _bookRepository.GetBooksAsync(cancellationToken);

            foreach (var book in books)
            {
                await _elasticSearchService.IndexAsync(book, "books");
            }
        }
    }
}
