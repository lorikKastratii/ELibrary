using AutoMapper;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Requests.Author;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Books.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorService authorService, IMapper mapper, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _mapper = mapper;
            _logger = logger;
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
        public async Task<IActionResult> GetAuthors()
        {
            var response = await _authorService.GetAuthorsAsync();

            if (response.IsSuccess is false)
            {
                _logger.LogError("Error fetching authors: {error}", response.Error?.Message);
                return NotFound(response.Error?.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest request)
        {
            if (request is null)
            {
                _logger.LogError("UpdateAuthor request is null.");
                return BadRequest("Request cannot be null");
            }

            var authorDto = _mapper.Map<AuthorDto>(request);

            var response = await _authorService.UpdateAuthorAsync(authorDto);

            if (response.IsSuccess)
            {
                return Ok("Author updated successfully.");
            }

            return StatusCode(500, "Failed to update author");
        }

        [HttpGet("GetAuthorBooks")]
        public async Task<IActionResult> GetAuthorBooks(int id)
        {
            var response = await _authorService.GetBooksByAuthorAsync(id);

            if (response.IsSuccess is false)
            {
                return NotFound();
            }

            return Ok(response.Data);
        }
    }
}
