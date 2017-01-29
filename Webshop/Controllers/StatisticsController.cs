using MVC.Controller;
using System;
using DataModels;
using DataModels.Statistics;
using MVC.View;
using Class_Diagram;

namespace Webshop.Controllers
{
    class StatisticsController : Controller
    {
        Context context = new Context();

        public ViewObject PostSalesAmountStatistics()
        {
            var data = GetBodyFromJson<DateJsonDataModel>();

            return Json(context.Orders.GetOrderAmountDataTask(data.TimeScale, data.BeginDate, data.EndDate));
        }

        public ViewObject GetTimeScales()
        {
            return Json(Enum.GetNames(typeof(TimeScale)));
        }

        public ViewObject PostGenreAmountStatistics()
        {
            var data = GetBodyFromJson<DateJsonDataModel>();
            var genres = context.Genres.GetAll().Result;
            return Json(context.Orders.GetPopularGenreOfOrdersStatistics(data.BeginDate, data.EndDate, data.TimeScale, genres));
        }

        public ViewObject PostWishListStatistics()
        {
            var data = GetBodyFromJson<GerneJsonDataModel>();
            var genre = data.Gerne != "All" ? context.Genres.GetByTitle(data.Gerne).Result : null;
            var statistics = context.Users.GetGameWishListStatistics(genre);
            return Json(statistics);
        }
    }
}
