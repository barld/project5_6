using DataModels;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Controllers
{
    public class PlatformController : MVC.Controller.Controller
    {
        private Context context;

        public override void AfterConstruct()
        {
            base.AfterConstruct();
            this.context = new Context();
        }

        public ViewObject GetAll()
        {
            return Json(context.Platforms.GetAll().Result);
        }
    }
}
