using ELibrary.Orders.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Orders.PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser{username}")]
        public async Task<IActionResult> GetUserAsync(string username)
        {
            var user = await _userService.GetUserByEmail(username);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
