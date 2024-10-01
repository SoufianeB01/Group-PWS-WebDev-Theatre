using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        public static List<User> Users = new List<User>();

        [HttpPost]
        public IActionResult Register([FromBody] User newUser)
        {
            if (Users.Exists(u => u.Username == newUser.Username))
            {
                return BadRequest("Username already exists.");
            }

            newUser.Id = Users.Count + 1;
            Users.Add(newUser);
            return Ok("User registered successfully.");
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            return Ok(Users);
        }
    }

}