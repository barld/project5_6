using MVC;
using DataModels;
using Webshop.Models;
using System;

namespace Webshop
{
    internal class AuthModule
    {
        const string currentUserKey = "currentUser";

        private Session session;
        private readonly Context context;
        public User CurrentUser
        {
            get
            {
                if (session.Data.ContainsKey(currentUserKey))
                    return session.Data[currentUserKey] as User;
                else
                    return default(User);
            }
            set
            {
                if (session.Data.ContainsKey(currentUserKey))
                    session.Data[currentUserKey] = value;
                else
                    session.Data.Add(currentUserKey, value);
            }
        }

        public bool LoggedIn
        {
            get
            {
                return CurrentUser != null && CurrentUser.AccountRole != AccountRole.Guest;
            }
        }

        public AuthModule(Session session, Context context)
        {
            this.session = session;
            this.context = new Context();
        }

        public ActionResultViewModel Login(User user)
        {
            if (session.Data.ContainsKey("currentUser"))
            {
                return new ActionResultViewModel { Succes = false, Message = "user already loged in" };
            }
            else
            {
                var logedinUser = context.Users.Login(user.Email, user.Password).Result;
                if (logedinUser != null)
                {
                    session.Data.Add("currentUser", logedinUser);
                    return new ActionResultViewModel { Succes = true, Message = "user succesfoly logged in" };
                }
                else
                {
                    return new ActionResultViewModel { Succes = false, Message = "no correct login data" };
                }
            }            
        }

        public ActionResultViewModel Register(User user)
        {
            try
            {
                CurrentUser = context.Users.Register(user.Email, user.Password, user.Gender).Result;
                return new ActionResultViewModel { Succes = true, Message = "succes registerd user" };
            }
            catch(Exception e)
            {
                return new ActionResultViewModel { Succes = false, Message = "sommthing went wrong" };
            }
        }

        public bool Logout()
        {
            if (CurrentUser == null || CurrentUser.AccountRole == AccountRole.Guest)
                return true;
            else
            {
                CurrentUser = new User { AccountRole = AccountRole.Guest };
                return true;
            }
            
        }

        public IUserLoginStatus LoginStatus()
        {
            if (LoggedIn)
            {
                return new UserLogedInStatus { Email = CurrentUser.Email };
            }
            else
            {
                return new UserNotLogedInStatus();
            }
        }

    }
}