using AutoMapper;
using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Category;
using ELibrary.Books.Application.Models;
using ELibrary.Books.Application.Requests;

namespace ELibrary.Books.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryRequest, CreateCategory>();
            CreateMap<CreateCategory, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
