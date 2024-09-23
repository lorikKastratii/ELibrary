using AutoMapper;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Requests.Book;
using Microsoft.AspNetCore.Mvc;

namespace ELibary.Books.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var response = await _bookService.GetBooksAsync();

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Error?.Message);
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var bookDto = _mapper.Map<BookDto>(request);

            var response = await _bookService.CreateBookAsync(bookDto);

            if (response.IsSuccess)
            {
                _logger.LogWarning("Failed to create book with title: {title}", request.Title);

                return Ok(response.Error?.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost("UpdateBook")]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest request)
        {
            if (request is null)
            {
                _logger.LogWarning("UpdateBookRequest is null!");

                return BadRequest();
            }

            var book = _mapper.Map<BookDto>(request);

            var response = await _bookService.UpdateBookAsync(book);

            if (response is false)
            {
                return BadRequest("Updating book failed");
            }

            return Ok("Book updated successfully!");
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookByIdAsync(id, cancellationToken);

            if (book is not null)
            {
                return Ok(book);
            }

            return NotFound("Book does not exists");
        }

        [HttpGet("GetBooksByCategory{id}")]
        public async Task<IActionResult> GetBooksByCategory(int id)
        {
            var books = await _bookService.GetBooksByCategoryAsync(id);

            if (books is not null && books.Count() > 1)
            {
                return Ok(books);
            }

            return NotFound("No book exists in this category");
        }

    }
}
