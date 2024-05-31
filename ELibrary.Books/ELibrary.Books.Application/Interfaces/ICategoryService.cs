using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Extensions;
using ELibrary.Books.Application.Models;

namespace ELibrary.Books.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<bool>> AddCategoryAsync(CreateCategory categoryModel);
    }
}
