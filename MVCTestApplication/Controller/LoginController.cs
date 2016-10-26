using MVC.Controller;
using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;

namespace MVC.DevelopmentTest.Controller
{
    class LoginController : MVC.Controller.Controller
    {
        public string Get()
        {
            return "<h1>Login Page</h1>";
        }

        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }
            public string response { get; set; }
        }

        public object PostLogin()
        {
            var data = GetBodyFromJson<Login>();

            if (data.username == "user" && data.password == "secret")
            {
                Cookie cookie = new Cookie("test", "Authenticated");
                data.response = cookie.Value;
            }

            return Json(data.response);
        }
    }
}
