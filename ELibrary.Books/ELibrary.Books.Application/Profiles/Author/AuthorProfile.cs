using AutoMapper;
using ELibrary.Books.Application.Dtos.Author;
using ELibrary.Books.Application.Requests.Author;

using AuthorEntity = ELibrary.Books.Domain.Entity.Author;

namespace ELibrary.Books.Application.Profiles.Author
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, AuthorEntity>()
                .ReverseMap();
            CreateMap<CreateAuthorRequest, AuthorDto>();
            CreateMap<UpdateAuthorRequest, AuthorDto>();
        }
    }
}
