using DataModels;
using MVC.Controller;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Controllers
{
    public class UserAdminController : Controller
    {
        AuthModule auth;
        IContext context;



        public override void AfterConstruct()
        {
            base.AfterConstruct();

            context = new Context();
            auth = new AuthModule(Session, context);
            if (auth.CurrentUser.AccountRole != AccountRole.Admin)
                throw new NotSupportedException("Controlle is not supported for a user with this type of accountrole");

        }

        public ViewObject GetAllUsers() 
            => Json(context.Users.GetAll().Result);

    }
}
