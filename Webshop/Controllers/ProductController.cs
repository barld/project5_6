using MVC.Controller;
using MVC.View;
using System;
using DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Controllers
{
    class ProductController : Controller
    {
        Context context = new Context();

        public ViewObject Get()
        {
            return Json(new {IsVRCompatible = true, Price = 45.00f, Publisher = "EA", RatingPegi = ".", ReleaseDate = new DateTime()});
        }

        public ViewObject GetAll()
        {
            IEnumerable<Game> games = context.Games.GetAll().Result;
            return Json(games);
        }

        public ViewObject PostSearch()
        {
            var data = GetBodyFromJson<Game>();
            IEnumerable<Game> game = context.Games.GetByTitleLike(data.GameTitle).Result;

            return Json(game);
        }
    }
}
