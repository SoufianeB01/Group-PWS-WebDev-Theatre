using Microsoft.AspNetCore.Mvc;
using TheatreSystem.Services;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (_userService.ValidateAdminCredentials(loginRequest.Username, loginRequest.Password))
            {
                _userService.RegisterSession(loginRequest.Username);
                return Ok("Login successful.");
            }

            return BadRequest("Invalid username or password.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _userService.Logout();
            return Ok("Logged out successfully.");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
