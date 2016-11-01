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
        private AuthModule Auth;
        private readonly Context context;

        public UserController()
        {
            context = new Context();
         }
        public override void AfterConstruct()
        {
            Auth = new AuthModule(Session, context);
        }

        public ViewObject PostLogin()
        {
            var data = GetBodyFromJson<User>();
            return Json(Auth.Login(data));

        }

        public bool GetLogout()
        {
            return Auth.Logout();
        }

        //public string GetAll()
        //{
        //    var userList = usermodel.userAll();
        //    string response = "";
        //    int cnt = 0;

        //    foreach (User user in userList)
        //    {
        //        cnt++;
        //        response += "<br> User " + cnt + " = " + user.Email;
        //    }

        //    return response;
        //}

        //public object PostSearch()
        //{
        //    var data = GetBodyFromJson<User>();
        //    string response = "";
        //    var userList = usermodel.userSearch(data.Email);

        //    foreach (User user in userList)
        //    {
        //        response += user.Email + "<br>";
        //    }

        //    return response;
        //}

        public ViewObject PostRegister()
        {
            var user = GetBodyFromJson<User>();


            return Json(Auth.Register(user));
        }

        public string Get()
        {
            return "<h1>Login Page</h1>";
        }
    }
}
