using MVC;
using DataModels;

namespace Webshop
{
    internal class AuthModule
    {
        private Session session;
        bool loggedIn;
        private readonly Context context;

        public bool LoggedIn
        {
            get { return this.loggedIn; }
        }

        public AuthModule(Session session)
        {
            this.session = session;
            this.context = new Context();
        }

        public string Login(User user)
        {
            if (session.Data.ContainsKey("login"))
            {
                return "<h1>Logged in : " + session.Data["login"].ToString() + "</h1>";
            }
            var logedinUser = context.Users.Login(user.Email, user.Password).Result;
            if(logedinUser != null)
            {
                session.Data.Add("currentUser", logedinUser);
            }
            session.Data.Add("login", user.Email);
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

        public AccountRole UserRole(User user)
        {
            return user.AccountRole;
        }

    }
}