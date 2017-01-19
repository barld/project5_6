using MVC.Controller;
using MVC.View;
using System;
using DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using Class_Diagram;

namespace Webshop.Controllers
{
    public class ProductController : Controller
    {
        Context context = new Context();

        public JsonDataView Get()
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
            var data = GetBodyFromJson<SearchGameModel>();
            //Game game = context.Games.GetByTitle(data.GameTitle).Result;
            IEnumerable<Game> game = context.Games.GetByTitleLike(data).Result;

            return Json(game);
        }

        public object GetGamebyPlatform()
        {
            string platformTitle = this.Parameters.ContainsKey("pt") ? this.Parameters["pt"] : string.Empty;
            Platform platform = context.Platforms.GetByTitle(platformTitle).Result;
            IEnumerable<Game> games = context.Games.GetAllByPlatform(platform).Result;
            return Json(games);
        }

        public ViewObject PostGame()
        {
            Game game = this.GetBodyFromJson<Game>();
            context.Games.Insert(game).Wait();

            return Json(game);
        }

        public ViewObject DeleteGame()
        {
            Game game = this.GetBodyFromJson<Game>();
            context.Games.Delete("EAN", game.EAN.ToString()).Wait();
            return Json(game);
        }

        public async void GetEditGame()
        {
            Game game = this.GetBodyFromJson<Game>();
            await context.Games.Replace(game.EAN.ToString(), game.EAN.ToString(), game);    
        }
    }
}
