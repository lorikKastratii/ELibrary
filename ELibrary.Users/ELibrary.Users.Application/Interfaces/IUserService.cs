using ELibrary.Users.Application.Dtos;
using ELibrary.Users.Application.Extensions;
using ELibrary.Users.Application.Requests;
using ELibrary.Users.Domain.Entities;

namespace ELibrary.Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> RegisterUser(UserRegisterDto user);
        Task<ServiceResponse<string>> AuthenticateUser(string email, string password);
        Task<ServiceResponse<UserDto>> GetUser(int id);
    }
}
