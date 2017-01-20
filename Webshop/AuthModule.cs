using MVC;
using DataModels;
using Webshop.Models;
using System;

namespace Webshop
{
    public class AuthModule
    {
        const string currentUserKey = "currentUser";

        private Session session;
        private readonly IContext context;
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

        public AuthModule(Session session, IContext context)
        {
            this.session = session;
            this.context = context;
            if (LoggedIn) // get a new version of the user from the database
            {
                CurrentUser = context.Users.GetByEmail(CurrentUser.Email).Result;
            }
        }

        public ActionResultViewModel Login(User user)
        {
            if (LoggedIn)
            {
                //Return an error if the user is already logged in
                return new ActionResultViewModel { Success = false, Message = "User already logged in" };
            }
            else
            {
                var logedinUser = context.Users.Login(user.Email, user.Password).Result;
                if (logedinUser != null && logedinUser.AccountRole == AccountRole.User)
                {
                    CurrentUser = logedinUser;
                    //Return a succes message when the user succesfully logs in
                    return new ActionResultViewModel { Success = true, Message = "User succesfully logged in" };
                }
                else if (logedinUser != null && logedinUser.AccountRole == AccountRole.Admin)
                {
                    CurrentUser = logedinUser;

                    //Return a success message whenthe admin successfully logs in
                    return new ActionResultViewModel { Success = true, Message = "Admin succesfully logged in", Admin = true};
                }
                else
                {
                    //Return an error when the login fails
                    return new ActionResultViewModel { Success = false, Message = "Incorrect login data" };
                }
            }            
        }

        public ActionResultViewModel Register(User user)
        {
            try
            {
                CurrentUser = context.Users.Register(user.Email, user.Password, user.Gender).Result;
                //Return a succes message when the user succesfully registers a new account
                return new ActionResultViewModel { Success = true, Message = "Succesfully registered user" };
            }
            catch(Exception e)
            {
                //Return an error when the registration fails
                return new ActionResultViewModel { Success = false, Message = "Something went wrong" };
            }
        }

        public bool Logout()
        {
            //Check what role the user has when they want to logout
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
            //Check if the user is logged in
            if (LoggedIn)
            {
                return new UserLogedInStatus { Email = CurrentUser.Email, Role = CurrentUser.AccountRole.ToString() };
            }
            else
            {
                return new UserNotLogedInStatus();
            }
        }

    }
}