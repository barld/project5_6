using MVC;

namespace Webshop
{
    internal class AuthModule
    {
        private Session session;

        public AuthModule()
        {
        }

        public AuthModule(Session session)
        {
            this.session = session;
        }

        public bool IsLogedIn { get; internal set; }
    }
}