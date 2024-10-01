using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatreReservationSystem.Models;

namespace TheatreReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private static List<User> users = RegisterController.Users;

        [HttpPost]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = users.Find(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok($"Welcome, {user.Username}!");
        }
    }
}