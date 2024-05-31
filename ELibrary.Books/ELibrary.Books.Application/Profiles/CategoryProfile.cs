using AutoMapper;
using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Models;
using ELibrary.Books.Application.Requests;

namespace ELibrary.Books.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateBookRequest, CreateCategory>();
            CreateMap<CreateCategory, Category>();
        }
    }
}
