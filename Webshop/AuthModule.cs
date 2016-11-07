﻿using MVC;
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
                if (!session.Data.ContainsKey(currentUserKey))
                {
                    session.Data.Add(currentUserKey, new User { AccountRole = AccountRole.Guest });
                }
                return session.Data[currentUserKey] as User;
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
            if (LoggedIn)
            {
                return new ActionResultViewModel { Success = false, Message = "User already logged in" };
            }
            else
            {
                var logedinUser = context.Users.Login(user.Email, user.Password).Result;
                if (logedinUser != null)
                {
                    CurrentUser = logedinUser;
                    return new ActionResultViewModel { Success = true, Message = "User succesfully logged in" };
                }
                else
                {
                    return new ActionResultViewModel { Success = false, Message = "Incorrect login data" };
                }
            }            
        }

        public ActionResultViewModel Register(User user)
        {
            try
            {
                CurrentUser = context.Users.Register(user.Email, user.Password, user.Gender).Result;
                return new ActionResultViewModel { Success = true, Message = "Succesfully registered user" };
            }
            catch(Exception e)
            {
                return new ActionResultViewModel { Success = false, Message = "Something went wrong" };
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