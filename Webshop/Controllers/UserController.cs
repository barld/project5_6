using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using DataModels;

namespace Webshop.Controllers
{
    public class UserController : MVC.Controller.Controller
    {
        UserModel usermodel = new UserModel();
        Authentication Auth = new Authentication();

        //public ViewObject PostLogin()
        //{
        //    var data = GetBodyFromJson<LoginViewModel>();
        //    var model = new UserLogedInStatus { Email = data.Email };


        //    if (this.Session.Data.ContainsKey("loginStatus"))
        //    {
        //        Session.Data["loginStatus"] = model;
        //    }
        //    else
        //    {
        //        Session.Data.Add("loginStatus", model);
        //    }
        //    return Json(new LoginResultViewModel { Succes = true, message = "Succesvol ingelogd" });
        //}

        //public ViewObject GetLoginStatus()
        //{
        //    if (Session.Data.ContainsKey("loginStatus"))
        //    {
        //        return Json(Session.Data["loginStatus"]);
        //    }
        //    else
        //    {
        //        return Json(new UserNotLogedInStatus());
        //    }
        //}

        public string PostLogin()
        {
            var data = GetBodyFromJson<User>();
            var users = usermodel.userSearch(data.email);
            User user = new User();

            if (Auth.LoggedIn)
            {
                return Auth.LoginStatus();
            }

            if (users.Count() == 1)
            {
                user = users[0];
            }

            if (user.password == data.password)
            {
                return Auth.Login(user);
            }

            return "Login Failed";

        }

        public string GetLogout()
        {
            if (this.Session.Data.ContainsKey("login"))
            {
                return Auth.Logout();
            }

            return "Not Logged in.";
        }

        public string GetAll()
        {
            var userList = usermodel.userAll();
            string response = "";
            int cnt = 0;

            foreach (User user in userList)
            {
                cnt++;
                response += "<br> User " + cnt + " = " + user.userName;
            }

            return response;
        }

        public object PostSearch()
        {
            var data = GetBodyFromJson<User>();
            string response = "";
            var userList = usermodel.userSearch(data.email);

            foreach (User user in userList)
            {
                response += user.userName + "<br>";
            }

            return response;
        }

        public object PostRegister()
        {
            var data = GetBodyFromJson<User>();
            usermodel.userRegister(data);
            return Json(data);
        }

        public string Get()
        {
            return "<h1>Login Page</h1>";
        }
    }
}
