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
    public class GenreController : MVC.Controller.Controller
    {
        private readonly Context context;

        public GenreController()
        {
            context = new Context();
        }

        public ViewObject GetAll()
        {
            IEnumerable<Genre> genres = context.Genres.GetAll().Result;
            return Json(genres);
        }


        public ViewObject Post()
        {
            bool error = false;
            
            Genre genre = this.GetBodyFromJson<Genre>();

            if(genre.Name == "")
            {
                error = true;
                throw new Exception("No Name");
            }

            if(genre.Description == "")
            {
                error = true;
                throw new Exception("No Description");
            }

            if (!error)
            {
                context.Genres.Insert(genre).Wait();
            }

            return Json(genre);
        }
    }
}
