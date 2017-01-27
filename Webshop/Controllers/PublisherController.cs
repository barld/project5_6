using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using DataModels;
using MVC;

namespace Webshop.Controllers
{
    public class PublisherController : MVC.Controller.Controller
    {
        private readonly Context context;

        public PublisherController()
        {
            context = new Context();
        }

        //public ViewObject GetAll()
        //{
        //    //IEnumerable<Publisher> genres = context.Genres.GetAll().Result;
        //    //return Json(genres);
        //}
    }
}
