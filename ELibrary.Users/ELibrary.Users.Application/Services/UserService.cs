using AutoMapper;
using ELibrary.Users.Application.Dtos;
using ELibrary.Users.Application.Extensions;
using ELibrary.Users.Application.Interfaces;
using ELibrary.Users.Application.Requests;
using ELibrary.Users.Domain.Entities;
using ELibrary.Users.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ELibrary.Users.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IUserRepository userRepository,
            IJwtService jwtService, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> RegisterUser(UserRegisterDto request)
        {
            if (request is null)
            {
                return new ServiceResponse<string>(Errors.USER_NOT_CREATED);
            }

            var user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token = _jwtService.GenerateJwtToken(user);

                return new ServiceResponse<string> { Data = token };
            }

            return new ServiceResponse<string>(Errors.USER_NOT_CREATED);
        }

        public async Task<ServiceResponse<string>> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, password))
            {
                var token = _jwtService.GenerateJwtToken(user);

                return new ServiceResponse<string> { Data = token };
            }

            return new ServiceResponse<string>(Errors.USER_NOT_FOUND);
        }

        public async Task<ServiceResponse<UserDto>> GetUser(int id)
        {
            if (id < 0)
            {
                return new ServiceResponse<UserDto>(Errors.USER_NOT_FOUND);
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
                return new ServiceResponse<UserDto>(Errors.USER_NOT_FOUND);

            var userDto = _mapper.Map<UserDto>(user);

            return new ServiceResponse<UserDto> { Data = userDto };
        }
    }
}
