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

        public ViewObject Post()
        {
            bool error = false;

            Game game = this.GetBodyFromJson<Game>();

            if(game.GameTitle == "")
            {
                error = true;
                throw new Exception("No GameTitle filled in!");
            }

            if(game.Description == "")
            {
                error = true;
                throw new Exception("No Description filled in!");
            }

            if(!error)
            {
                context.Games.Insert(game).Wait();
            }

            return Json(game);
        }

        // DELETE: /api/product/
        public ViewObject Delete()
        {
            string gameEAN = this.Parameters.ContainsKey("ean") ? this.Parameters["ean"] : string.Empty;
            context.Games.Delete("GameTitle", gameEAN).Wait();
            return Json(gameEAN);
        }

        // PUT: /api/product/
        public async void Put()
        {
            Game game = this.GetBodyFromJson<Game>();
            await context.Games.Update(game);
        }
    }
}
