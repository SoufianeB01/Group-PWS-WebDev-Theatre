using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatreSystem.Models;

namespace TheatreSystem.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _Users;
        private string _loggedInUser;

        public UserService()
        {
            _Users = new List<User> ();
            _loggedInUser = null; 
        }

        public bool IsUserLoggedIn()
        {
            return _loggedInUser != null;
        }

        public void RegisterSession(string username)
        {
            _loggedInUser = username;  
        }

        public void Logout()
        {
            _loggedInUser = null;  
        }

        public void RegisterNewUser(User newUser)
        {
            newUser.Id = Guid.NewGuid();
            _Users.Add(newUser);
        }

        public bool UserExists(string username)
        {
            return _Users.Any(u => u.Username == username);
        }

        public User GetUserByUsername(string username)
        {
            return _Users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAllUsers()
        {
            return _Users;
        }
    }

    public interface IUserService
    {
        void RegisterSession(string username);
        void Logout();
        void RegisterNewUser(User newUser);
        bool UserExists(string username);
        User GetUserByUsername(string username);
        List<User> GetAllUsers();
    }
}
