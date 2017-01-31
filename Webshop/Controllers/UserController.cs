using MVC.View;
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
            //var data = GetBodyFromJson<User>();
            List<MyLists> myLists = context.Users.GetMyLists(Auth.CurrentUser).Result;
            return Json(myLists);
        }

        struct EANAndTitleOfList
        {
            public long EAN { set; get; }
            public string TitleOfList { get; set; }
        }

        public ViewObject PostAlreadyHaveGame()
        {
            //Retrieve My Lists from the user, this can contain lists such as Wish List/Favourite List and more custom lists
            var data = GetBodyFromJson<EANAndTitleOfList>();
            return Json(
                context.Users.GetMyLists(Auth.CurrentUser).Result
                ?.Find(l => l.TitleOfList == data.TitleOfList)
                ?.Games.Find(game => game.EAN == data.EAN) != null
            );
        }

        public ViewObject PostAddToList()
        {
            var data = GetBodyFromJson<EANAndTitleOfList>();
            Game game = context.Games.GetByEAN(data.EAN).Result;

            //Check if the user has no lists
            if (!Auth.CurrentUser.MyLists.Any())
            {
                List<MyLists> newList = new List<MyLists>()
                {
                    new MyLists() {TitleOfList = "Wish List", Games = new List<Game>()},
                    new MyLists() {TitleOfList = "Favourite List", Games = new List<Game>()}
                };
                Auth.CurrentUser.MyLists.AddRange(newList);
            }

            //Find the correct list
            MyLists list = Auth.CurrentUser.MyLists.First(x => x.TitleOfList == data.TitleOfList);

            //Make sure that the game is not already in the list to avoid duplicate games
            if (list.Games.FirstOrDefault(x => x.EAN == game.EAN) == null)
            {
                //Add game to list
                if (data.TitleOfList == "Favourite List")
                {
                    foreach (var order in context.Orders.GetAllByCustomer_id(Auth.CurrentUser._id).Result)
                    {
                        var result = order.OrderLines.FirstOrDefault(x => x.Game.EAN == game.EAN);
                        if (result != null)
                        {
                            list.Games.Add(game);
                            break;
                        }
                    }
                }
                else
                {
                    list.Games.Add(game);
                }

                //User updatedUser = Auth.CurrentUser;
                //updatedUser.MyLists.First(x => x.TitleOfList == list.TitleOfList).Games = list.Games;
                context.Users.Update(Auth.CurrentUser);
                return Json(true);
            }
            else
            {
                //Game already exists in the list, remove it
                var item = list.Games.FirstOrDefault(g => g.EAN == game.EAN);
                if (item != null)
                {
                    list.Games.Remove(item);
                }
                context.Users.Update(Auth.CurrentUser);
                return Json(false);
            }
        }

        public ViewObject GetOrders()
        {
            if (Auth.LoggedIn)
            {
                return Json(context.Orders.GetAllByCustomer_id(Auth.CurrentUser._id).Result);
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

        public ViewObject PostToggleSharedWishList()
        {
            MyLists wishList = this.GetBodyFromJson<MyLists>();
            var id = wishList._id;
            MyLists userList = Auth.CurrentUser.MyLists.FirstOrDefault(x => x._id == id);
            if (userList != null)
            {
                if (userList.IsPrivate)
                {
                    userList.IsPrivate = false;
                    context.Users.Update(Auth.CurrentUser);
                    return Json(false);
                }
                else
                {
                    userList.IsPrivate = true;
                    context.Users.Update(Auth.CurrentUser);
                    return Json(true);
                }
            }

            return null;
        }

        public ViewObject GetSharedWishList()
        {
            string wishListId = this.Parameters.ContainsKey("id") ? this.Parameters["id"] : string.Empty;

            var userWishList =
                context.Users.GetAll()
                    .Result.SelectMany(user => user.MyLists)
                    .FirstOrDefault(l =>
                        !l.IsPrivate &&
                        l._id == wishListId &&
                        l.TitleOfList == "Wish List"
                        );
            return Json(userWishList);
        }
    }
}
