using AutoMapper;
using ELibrary.BookCatalog.Dto;
using ELibrary.BookCatalog.Entity;
using ELibrary.BookCatalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.BookCatalog.Controllers
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
        [Route("GetBook{id}")]
        public IActionResult GetBook(int id)
        {
            var response = _bookService.GetBook(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return NotFound(response.Error?.Message);
        }

        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks()
        {
            var response = _bookService.GetAllBooks();

            if (response is not null)
            {
                return Ok(response);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("CreateBook")]
        public IActionResult CreateBook(CreateBookRequest request)
        {
            var book = _mapper.Map<Book>(request);

            var response = _bookService.CreateBook(book);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("UpdateBook{id}")]
        public IActionResult UpdateBook(UpdateBookRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(request);

            var response = _bookService.UpdateBook(book);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteBook{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = _bookService.DeleteBook(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("SearchBook{title}")]
        public IActionResult DeleteBook(string title)
        {
            var result = _bookService.SearchBook(title);

            if (result.Any() is false || result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
