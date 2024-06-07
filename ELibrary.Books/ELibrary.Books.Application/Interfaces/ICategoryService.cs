using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Category;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Models;

namespace ELibrary.Books.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CreateCategory categoryModel);
    }
}
