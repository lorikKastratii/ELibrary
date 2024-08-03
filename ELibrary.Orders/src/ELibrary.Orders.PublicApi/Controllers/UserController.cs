using ELibrary.Orders.Application.Clients.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Orders.PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserClient _userService;

        public UserController(IUserClient userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user is null)
            {
                return NotFound("User with this Id does not exists.");
            }

            return Ok(user);
        }
    }
}
