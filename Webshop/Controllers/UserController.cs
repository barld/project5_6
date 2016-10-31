using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using DataModels;
using MVC;

namespace Webshop.Controllers
{
    public class UserController : MVC.Controller.Controller
    {
        UserModel usermodel = new UserModel();

        public string PostLogin()
        {
            AuthModule Auth = new AuthModule(this.Session);
            var data = GetBodyFromJson<User>();
            var users = usermodel.userSearch(data.Email);

            if (users.Count() == 1)
            {
                User user = users[0];

                if (user.Password == data.Password)
                {
                    return Auth.Login(user);
                }
            }

            return "Login Failed";

        }

        public string GetLogout()
        {
            AuthModule Auth = new AuthModule(this.Session);
            return Auth.Logout();
        }

        public string GetAll()
        {
            var userList = usermodel.userAll();
            string response = "";
            int cnt = 0;

            foreach (User user in userList)
            {
                cnt++;
                response += "<br> User " + cnt + " = " + user.Email;
            }

            return response;
        }

        public object PostSearch()
        {
            var data = GetBodyFromJson<User>();
            string response = "";
            var userList = usermodel.userSearch(data.Email);

            foreach (User user in userList)
            {
                response += user.Email + "<br>";
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
