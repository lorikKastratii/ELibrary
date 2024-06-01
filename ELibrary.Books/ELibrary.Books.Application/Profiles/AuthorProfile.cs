using AutoMapper;
using Elibrary.Books.Domain.Entity;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Requests;

namespace ELibrary.Books.Application.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, Author>();
            CreateMap<CreateAuthorRequest, AuthorDto>();
        }
    }
}
