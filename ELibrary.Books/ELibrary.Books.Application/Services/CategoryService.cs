using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepositor, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepositor;
            _logger = logger;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            if (category is null)
            {
                return false;
            }

            try
            {
                var response = await _categoryRepository.AddAsync(category);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error happened while creating category with name: {name}", category.Name);

                return false;
            }
        }
    }
}
