using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatreSystem.Controllers;
using TheatreSystem.Models;

namespace TheatreReservationSystem.Controllers
{
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        public static List<User> Users = RegisterController.Users;
        
        [HttpPost]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = Users.Find(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok($"Welcome, {user.Username}!");
        }
    }
}