using AutoMapper;
using ELibrary.Books.Application.Dtos;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Books.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
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
    }
}
