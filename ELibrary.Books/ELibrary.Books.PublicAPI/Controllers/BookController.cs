using AutoMapper;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Requests.Book;
using ELibrary.Books.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ELibrary.Books.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        private readonly IElasticClient _elasticClient;

        public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger, IElasticClient elasticClient)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
            _elasticClient = elasticClient;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooksAsync(CancellationToken cancellationToken)
        {
            var response = await _bookService.GetBooksAsync(cancellationToken);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Error?.Message);
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var bookDto = _mapper.Map<BookDto>(request);

            var response = await _bookService.CreateBookAsync(bookDto, cancellationToken);

            if (response.IsSuccess)
            {
                _logger.LogWarning("Failed to create book with title: {title}", request.Title);

                return Ok(response.Error?.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost("UpdateBook")]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                _logger.LogWarning("UpdateBookRequest is null!");

                return BadRequest();
            }

            var book = _mapper.Map<BookDto>(request);

            var response = await _bookService.UpdateBookAsync(book, cancellationToken);

            if (response.IsSuccess is false)
            {
                return BadRequest("Updating book failed");
            }

            return Ok("Book updated successfully!");
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var response = await _bookService.GetBookByIdAsync(id, cancellationToken);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return NotFound("Book does not exists");
        }

        [HttpGet("GetBooksByCategory{id}")]
        public async Task<IActionResult> GetBooksByCategory(int id, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetBooksByCategoryAsync(id, cancellationToken);

            if (books is not null)
            {
                return Ok(books);
            }

            return NotFound("No book exists in this category");
        }

        [HttpPost("populate-elastic")]
        public async Task<IActionResult> PopulateElasticsearch(CancellationToken cancellationToken = default)
        {
            try
            {
                await _bookService.PopulateElasticWithBooksAsync(cancellationToken);
                return Ok("Elasticsearch has been populated with books.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error populating Elasticsearch: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string query)
        {
            var books = await _bookService.SearchBooksAsync(query);
            return Ok(books);
        }
    }
}
