using AutoMapper;
using ELibrary.Books.Application.Dtos;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Books.PublicAPI.Controllers
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
                return Ok(response.Data);
            }

            return BadRequest(response.Error?.Message);
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
                _logger.LogWarning("Failed to update book with Id: {id}", request.Id);

                return BadRequest("Updating book failed");
            }

            return Ok("Book updated successfully!");
        }

        [HttpGet("GetBookById{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book is not null)
            {
                return Ok(book);
            }

            return NotFound();
        }
    }
}
