using System.Collections.Generic;
using System.Linq;

namespace TheatreSystem.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly List<Admin> _admins;
        private Admin _loggedInAdmin;

        public AuthorizationService()
        {
            _admins = new List<Admin>
            {
                new Admin { AdminID = 1, Username = "admin", Password = "password123", Email = "admin@gmail.com" },
                new Admin { AdminID = 2, Username = "admin2", Password = "password12345", Email = "admin2@gmail.com" }
            };
            _loggedInAdmin = null;
        }

        public bool IsLoggedIn()
        {
            return _loggedInAdmin != null;
        }

        public Admin GetLoggedInAdmin()
        {
            return _loggedInAdmin;
        }

        public bool Login(string username, string password)
        {
            var admin = _admins.FirstOrDefault(_ => _.Username == username);
            if (admin == null || admin.Password != password)
            {
                return false;
            }

            _loggedInAdmin = admin;
            return true;
        }

        public void Logout()
        {
            _loggedInAdmin = null;
        }
    }

    public interface IAuthorizationService
    {
        bool IsLoggedIn();
        Admin GetLoggedInAdmin();
        bool Login(string username, string password);
        void Logout();
    }
}
