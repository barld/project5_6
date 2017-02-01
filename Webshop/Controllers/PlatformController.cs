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

        public ViewObject Post()
        {
            bool error= false;

            Platform platform = this.GetBodyFromJson<Platform>();

            if(platform.Abbreviation == "")
            {
                error = true;
                throw new Exception("No Abbrevation");
            }

            if(platform.Brand == "")
            {
                error = true;
                throw new Exception("No Brand");
            }

            if(platform.Description == "")
            {
                error = true;
                throw new Exception("No Description");
            }

            
            if(!error)
            {
                context.Platforms.Insert(platform).Wait();
            }

            return Json(platform);
        }
    }
}
