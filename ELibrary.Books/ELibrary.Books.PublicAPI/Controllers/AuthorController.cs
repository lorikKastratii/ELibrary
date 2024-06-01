using AutoMapper;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Books.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var response = await _authorService.GetAuthorByIdAsync(id);

            if (response.IsSuccess is false)
            {
                return NotFound();
            }

            return Ok(response.Data);
        }

        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var author = _mapper.Map<AuthorDto>(request);

            var response = await _authorService.CreateAuthorAsync(author);

            if (response.IsSuccess is false)
            {
                return Ok(response.Error?.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet("GetAuthors")]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            var response = await _authorService.GetAuthorsAsync();

            if(response.IsSuccess is false)
            {
                return Ok(response.Error?.Message);
            }

            return Ok(response.Data);
        }
    }
}
