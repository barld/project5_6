using MVC.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Controller;
using MVC.View;
using MVC;

namespace Webshop
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class AuthorizeAttribute : Attribute, IFilterControllerAttribute
    {
        private AuthModule authModule;

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236

        // This is a positional argument
        public AuthorizeAttribute()
        {
            
        }

        ControllerObject IFilterControllerAttribute.Construct(Func<ControllerObject> constructFunc, Session session)
        {
            authModule = new AuthModule(session, new DataModels.Context());

            if (!authModule.LoggedIn)
                return null;
            else
                return constructFunc();
        }

        ViewObject IFilterControllerAttribute.HandeleAction(Func<ViewObject> action)
        {
            if (!authModule.LoggedIn)
                return new AccesDeniedView();
            else
                return action();
        }
    }
}
