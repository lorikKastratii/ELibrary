using AutoMapper;
using ELibrary.BookCatalog.Dto;
using ELibrary.BookCatalog.Entity;

namespace ELibrary.BookCatalog.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>();
        }
    }
}
