﻿using MVC.View;
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

        public ViewObject GetStatus()
        {
            return Json(this.Auth.LoginStatus());
        }

        public ViewObject PostRetrieveMyLists()
        {
            //Retrieve My Lists from the user, this can contain lists such as Wish List/Favourite List and more custom lists
            var data = GetBodyFromJson<User>();
            List<MyLists> myLists = context.Users.GetMyListsByEmail(data.Email).Result;
            return Json(myLists);
        }

        private class GameUser
        {
            public Game Game { get; set; }
            public User User { get; set; }
        }

        public ViewObject AddToList()
        {
            var data = GetBodyFromJson<GameUser>();
            Game game = context.Games.GetByEAN(data.Game.EAN).Result;
            User user = context.Users.GetByEmail(data.User.Email).Result;
            return Json(game);
        }
    }
}
