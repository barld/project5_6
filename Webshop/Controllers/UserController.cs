using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class UserController : MVC.Controller.Controller
    {
        public ViewObject PostLogin()
        {
            var data = GetBodyFromJson<LoginViewModel>();

            var model = new UserLogedInStatus { Email = data.Email };

            if (this.Session.Data.ContainsKey("loginStatus"))
            {
                Session.Data["loginStatus"] = model;
            }
            else
            {
                Session.Data.Add("loginStatus", model);
            }
            return Json(new LoginResultViewModel { Succes = true, message = "Succesvol ingelogd" });
        }

        public ViewObject GetLoginStatus()
        {
            if (Session.Data.ContainsKey("loginStatus"))
            {
                return Json(Session.Data["loginStatus"]);
            }
            else
            {
                return Json(new UserNotLogedInStatus());
            }

        }
    }
}
