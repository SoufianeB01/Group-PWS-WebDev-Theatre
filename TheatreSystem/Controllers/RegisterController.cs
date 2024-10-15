using Microsoft.AspNetCore.Mvc;
using TheatreSystem.Models;
using TheatreSystem.Services;

namespace TheatreSystem.Controllers
{
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] User newUser)
        {
            if (_userService.UserExists(newUser.Username))
            {
                return BadRequest("Username already exists.");
            }

            _userService.RegisterNewUser(newUser);
            return Ok("User registered successfully.");
        }
    }
}
