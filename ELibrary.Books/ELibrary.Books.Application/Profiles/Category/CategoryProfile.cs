using AutoMapper;
using ELibrary.Books.Application.Dtos.Category;
using ELibrary.Books.Application.Models;
using ELibrary.Books.Application.Requests.Category;

using CategoryEntity = ELibrary.Books.Domain.Entity.Category;

namespace ELibrary.Books.Application.Profiles.Category
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryRequest, CreateCategory>();
            CreateMap<CreateCategory, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryDto>();
        }
    }
}
