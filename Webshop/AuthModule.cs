﻿using MVC;
using DataModels;

namespace Webshop
{
    internal class AuthModule
    {
        private Session session;
        bool loggedIn;

        public bool LoggedIn
        {
            get { return this.loggedIn; }
        }

        public AuthModule(Session session)
        {
            this.session = session;
        }

        public string Login(User user)
        {
            if (session.Data.ContainsKey("login"))
            {
                return "<h1>Logged in : " + session.Data["login"].ToString() + "</h1>";
            }
            session.Data.Add("login", user.email);
            this.loggedIn = true;
            return "Login Successful";
        }

        public string Logout()
        {
            if (session.Data.ContainsKey("login"))
            {
                session.Data.Remove("login");
                this.loggedIn = false;
                return "Logout Successful";
            }

            return "Not Logged in.";
            
        }

        public string LoginStatus()
        {
            if (session.Data.ContainsKey("login"))
            {
                return "<h1>Logged in : " + session.Data["login"].ToString() + "</h1>";
            }
            else
            {
                return "<h1>Not Logged in.</h1>";
            }
        }

        public string UserRole(User user)
        {
            return user.role;
        }

    }
}