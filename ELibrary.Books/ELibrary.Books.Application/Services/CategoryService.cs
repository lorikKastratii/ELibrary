using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos.Category;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Extensions.Errors;
using ELibrary.Books.Application.Interfaces;
using ELibrary.Books.Application.Models;
using Microsoft.Extensions.Logging;

namespace ELibrary.Books.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepositor, ILogger<CategoryService> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepositor;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CreateCategory createCategory)
        {
            if (createCategory is null)
            {
                return new ServiceResponse<CategoryDto>(CategoryErrors.CATEGORY_EMPTY);
            }

            var categoryEntity = _mapper.Map<Category>(createCategory);

            try
            {
                var category = await _categoryRepository.CreateAsync(categoryEntity);

                if (category is null)
                {
                    _logger.LogError("Error creating category with name: {name}", createCategory.Name);

                    return new ServiceResponse<CategoryDto>(CategoryErrors.CATEGORY_CREATION_ERROR);
                }

                return new ServiceResponse<CategoryDto>(_mapper.Map<CategoryDto>(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error happened while creating category with name: {name}", createCategory.Name);

                return new ServiceResponse<CategoryDto>(CategoryErrors.CATEGORY_CREATION_ERROR);
            }
        }
    }
}
