using Microsoft.AspNetCore.Mvc;
using TheatreSystem.Models;
using TheatreSystem.Services;

namespace TheatreSystem.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (_userService.UserExists(newUser.Username))
            {
                return BadRequest("Username already exists.");
            }

            _userService.RegisterNewUser(newUser);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = _userService.GetUserByUsername(loginUser.Username);
            
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            
            if (user.Password != loginUser.Password)
            {
                return Unauthorized("Invalid password.");
            }
            
            _userService.RegisterSession(loginUser.Username);
            return Ok($"Welcome, {loginUser.Username}!");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _userService.Logout();  
            return Ok("Logged out successfully.");
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
