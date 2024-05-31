using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Extensions;
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

        public Task<ServiceResponse<bool>> AddCategoryAsync(CreateCategory categoryModel)
        {
            throw new NotImplementedException();
        }

        //public async Task<ServiceResponse<bool>> AddCategoryAsync(CreateCategory categoryModel)
        //{
        //    if (categoryModel is null)
        //    {
        //        return false;
        //    }

        //    var category = _mapper.Map<Category>(categoryModel);

        //    try
        //    {
        //        var response = await _categoryRepository.AddAsync(category);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error happened while creating category with name: {name}", category.Name);

        //        return false;
        //    }
        //}
    }
}
