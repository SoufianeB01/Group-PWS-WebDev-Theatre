using System;
using System.Collections.Generic;
using System.Linq;
using TheatreSystem.Models;
using Microsoft.AspNetCore.Http;


namespace TheatreSystem.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> users;
        private readonly IHttpContextAccessor Context;

        public UserService(IHttpContextAccessor context)
        {
            users = new List<User>
            {
                new User { Username = "admin", Password = "admin123" },
                new User { Username = "user1", Password = "user123" }
            };
            Context = context;
        }

        public bool IsUserLoggedIn()
        {
            return Context.HttpContext.Session.GetString("user") != null;
        }

        public void RegisterSession(string username)
        {
            Context.HttpContext.Session.SetString("user", username);
        }

        public void RemoveSession()
        {
            Context.HttpContext.Session.Remove("user");
        }

        public string GetLoggedInUser()
        {
            return Context.HttpContext.Session.GetString("user");
        }

        public void Logout()
        {
            RegisterSession(null);
        }

        public void RegisterNewUser(User newUser)
        {
            newUser.Id = Guid.NewGuid();
            users.Add(newUser);
        }

        public bool UserExists(string username)
        {
            var user = users.Any(u => u.Username == username);
            return user;
        }

        public User GetUser(string username)
        {
            var user = users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }

        public List<User> GetAllUsers()
        {
            return users;
        }
    }

    public interface IUserService
    {
        void RegisterSession(string username);
        void Logout();
        void RegisterNewUser(User newUser);
        bool UserExists(string username);
        User GetUser(string username);
        List<User> GetAllUsers();
        bool IsUserLoggedIn();
        string GetLoggedInUser();
        void RemoveSession();
    }
}