using Elibrary.Books.Domain.Entity;

namespace ELibrary.Books.Application.Interfaces
{
    public interface ICategoryService
    {
        //TODO: add dto instead of entity
        Task<bool> AddCategoryAsync(Category category);
    }
}
