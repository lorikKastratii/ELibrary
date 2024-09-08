using AutoMapper;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Models;
using ELibrary.Books.Application.Requests.Category;
using Microsoft.AspNetCore.Mvc;

namespace ELibary.Books.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _mapper = mapper;
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request cannot be null");
            }

            var createCategory = _mapper.Map<CreateCategory>(request);

            var response = await _categoryService.CreateCategoryAsync(createCategory);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return Ok(response.Error?.Message);
        }
    }
}
