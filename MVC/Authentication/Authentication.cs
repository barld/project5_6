using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace MVC.Authentication
{
    public class Authentication : MVC.Controller.Controller
    {
        bool loggedIn;

        public bool LoggedIn
        {
            get { return this.loggedIn; }
        }

        //login/logout
        public string Login(User user)
        {
            Session.Data.Add("login", user.email);
            this.loggedIn = true;
            return "Login Successful";
        }

        public string Logout()
        {
            this.Session.Data.Remove("login");
            this.loggedIn = false;
            return "Logout Successful";
        }
        
        //user state : logged in/out
        public string LoginStatus()
        {
            if (LoggedIn)
            {
                return "<h1>Logged in : " + this.Session.Data["login"].ToString() + "</h1>";
            }
            else
            {
                return "<h1>Not Logged in.</h1>";
            }
        }
        
        //user role : user/admin
        public string UserRole(User user)
        {
            return user.role;
        }

    }
}
