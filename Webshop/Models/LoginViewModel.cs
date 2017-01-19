using DataModels;
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
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool Admin { get; set; }
    }

    public interface IUserLoginStatus
    {
        bool IsLogedIn { get;  }
    }

    public class UserLogedInStatus : IUserLoginStatus
    {
        public bool IsLogedIn => true;
        public string Role { get; set; }
        public string Email { get; set; }
    }

    public class UserNotLogedInStatus : IUserLoginStatus
    {
        public bool IsLogedIn => false;
    }

}
