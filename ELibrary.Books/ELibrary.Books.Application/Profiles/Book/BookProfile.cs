using AutoMapper;
using ELibrary.Books.Application.Dtos.Book;
using ELibrary.Books.Application.Requests.Book;

using BookEntity = ELibrary.Books.Domain.Entity.Book;

namespace ELibrary.Books.Application.Profiles.Book
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookEntity, BookDto>()
                .ReverseMap();
            CreateMap<CreateBookRequest, BookDto>();
            CreateMap<UpdateBookRequest, BookDto>();
        }
    }
}
