using Microsoft.AspNetCore.Mvc;
using TheatreSystem.Services;

namespace TheatreSystem.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthorizationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = userService.GetUser(loginUser.Username);

            if (user == null)
                return BadRequest("Admin not found.");

            if (user.Password != loginUser.Password)
                return Unauthorized("Invalid password.");

            if (userService.IsUserLoggedIn())
                return BadRequest("An admin is already logged in.");

            userService.RegisterSession(loginUser.Username);
            return Ok($"Welcome, {loginUser.Username}!");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (!userService.IsUserLoggedIn())
                return BadRequest("No admin is logged in.");

            userService.RemoveSession();
            return Ok("Admin successfully logged out.");
        }

        [HttpGet("session")]
        public IActionResult CheckSession()
        {
            var username = userService.GetLoggedInUser();
            if (username == null)
                return BadRequest("No user is logged in.");

            return Ok($"Session is active. User: {username}");
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (userService.UserExists(newUser.Username))
                return BadRequest("Username already exists.");

            userService.RegisterNewUser(newUser);
            return Ok("Admin registered successfully.");
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            /*eventueel instellen dat alleen de admin dit mag zien
            var username = userService.GetLoggedInUser();
            if (username == null || username != "admin")
            {
                return Unauthorized("Only the admin can view users.");
            }*/

            var users = userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] User updatedUser)
        {
            var user = userService.GetUser(updatedUser.Username);
            if (user == null)
                return NotFound("Admin not found.");

            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;

            return Ok("Admin updated successfully.");
        }

        [HttpDelete("delete/{username}")]
        public IActionResult DeleteUser(string username)
        {
            var user = userService.GetUser(username);
            if (user == null)
                return NotFound("Admin not found.");

            var loggedInUser = userService.GetLoggedInUser();
            if (loggedInUser == null || loggedInUser != "admin")
                return Unauthorized("Only the admin can delete users.");

            var users = userService.GetAllUsers();
            users.Remove(user);
            return Ok("Admin deleted successfully.");
        }
    }
}