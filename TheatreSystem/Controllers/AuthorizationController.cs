using Microsoft.AspNetCore.Mvc;
using TheatreSystem.Services;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Admin login)
        {
            if (_authorizationService.IsLoggedIn())
            {
                return BadRequest("An admin is already logged in.");
            }

            var success = _authorizationService.Login(login.Username, login.Password);

            if (!success)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok($"Welcome, {login.Username}!");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (!_authorizationService.IsLoggedIn())
            {
                return BadRequest("No admin is currently logged in.");
            }

            _authorizationService.Logout();
            return Ok("Logged out successfully.");
        }

        [HttpGet("status")]
        public IActionResult GetLoginStatus()
        {
            var isLoggedIn = _authorizationService.IsLoggedIn();

            if (!isLoggedIn)
            {
                return Ok(new { IsLoggedIn = false, LoggedInUser = (string)null });
            }

            var loggedInAdmin = _authorizationService.GetLoggedInAdmin();
            return Ok(new { IsLoggedIn = true, LoggedInUser = loggedInAdmin.Username });
        }
    }
}
