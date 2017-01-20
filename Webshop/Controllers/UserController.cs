﻿using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
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

        public async void PostAddToList()
        {
            var data = GetBodyFromJson<Game>();
            var data2 = GetBodyFromJson<MyLists>();
            Game game = context.Games.GetByEAN(data.EAN).Result;
            MyLists list = Auth.CurrentUser.MyLists.Where(x => x.TitleOfList == data2.TitleOfList).First();
            list.Games.Add(game);
            User updatedUser = Auth.CurrentUser;
            updatedUser.MyLists.Where(x => x.TitleOfList == list.TitleOfList).First().Games = list.Games;
            //updatedUser.MyLists = myListsUpdated;
            //Auth.CurrentUser.MyLists.Where(x => x.TitleOfList == data2.TitleOfList).GetEnumerator().Current.Games.Add(game);
            //Auth.CurrentUser.MyLists.Where(x => x.TitleOfList == data2.TitleOfList).First().Games.Add(game);
            await context.Users.UpdateUser(updatedUser);
        }

        public ViewObject GetOrders()
        {
            if (Auth.LoggedIn)
            {
                User user = Auth.CurrentUser;
                return Json(context.Orders.GetAllByEmail(user.Email).Result);
            }
            else
            {
                return Json("user not logged in");
            }
        }

        public ViewObject PostUser()
        {
            User user = this.GetBodyFromJson<User>();
            context.Users.Insert(user).Wait();
            return Json(user);
        }
    }
}
