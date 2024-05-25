using AutoMapper;
using ELibrary.Users.Application.Dtos;
using ELibrary.Users.Application.Requests;
using ELibrary.Users.Domain.Entities;

namespace ELibrary.Users.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterRequest, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
