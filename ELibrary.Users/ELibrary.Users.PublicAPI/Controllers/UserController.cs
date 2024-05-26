using AutoMapper;
using ELibrary.Users.Application.Dtos;
using ELibrary.Users.Application.Interfaces;
using ELibrary.Users.Application.Requests;
using ELibrary.Users.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Users.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var userRegisterDto = _mapper.Map<UserRegisterDto>(request);

            var response = await _userService.RegisterUser(userRegisterDto);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Error);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var response = await _userService.AuthenticateUser(model.Email, model.Password);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Error?.Message);
        }
        
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUser(id);

            if (response.IsSuccess is false)
            {
                return NotFound(response.Error?.Message);
            }

            return Ok(response.Data);
        }
    }
}
