using AutoMapper;
using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos;
using ELibrary.Books.Application.Requests;

namespace ELibrary.Books.Application.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ReverseMap();
            CreateMap<CreateBookRequest, BookDto>();
            CreateMap<UpdateBookRequest, BookDto>();
        }
    }
}
