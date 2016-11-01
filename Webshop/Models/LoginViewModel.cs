using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ActionResultViewModel
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
    }

    public interface IUserLoginStatus
    {
        bool IsLogedIn { get; set; }
    }

    public class UserLogedInStatus : IUserLoginStatus
    {
        public bool IsLogedIn { get { return true; } set { } }
        public string Email { get; set; }
    }

    public class UserNotLogedInStatus : IUserLoginStatus
    {
        public bool IsLogedIn { get { return false; } set { } }
    }

}
