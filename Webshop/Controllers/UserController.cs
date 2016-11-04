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

        public bool PutLogout()
        {
            return Auth.Logout();
        }

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
