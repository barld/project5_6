﻿using DataModels;
using MVC.Controller;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class UserAdminController : Controller
    {
        AuthModule auth;
        IContext context;



        public override void AfterConstruct()
        {
            base.AfterConstruct();

            context = new Context();
            auth = new AuthModule(Session, context);
            if (auth.CurrentUser.AccountRole != AccountRole.Admin)
                throw new NotSupportedException("Controlle is not supported for a user with this type of accountrole");

        }

        public ViewObject GetAllUsers() 
            => Json(context.Users.GetAll().Result);

        public ViewObject DeleteUser()
        {
            var user = GetBodyFromJson<User>();
            context.Users.Delete(user);
            return Json(new ActionResultViewModel { Success = true });
        }

        public ViewObject PutUser()
        {
            var user = GetBodyFromJson<User>();
            var dbUser = context.Users.GetById(user._id).Result;
            user.Salt = dbUser.Salt;
            user.Password = dbUser.Password;
            context.Users.Replace("_id", user._id, user);
            return Json(new ActionResultViewModel { Success = true });
        }

    }
}
