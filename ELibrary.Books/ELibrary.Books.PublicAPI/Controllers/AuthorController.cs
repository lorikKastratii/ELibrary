using ELibrary.Books.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Books.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author is null)
            {
                return NotFound();
            }

            return Ok(author);
        }

    }
}
