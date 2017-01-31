using MVC.Controller;
using System;
using DataModels;
using DataModels.Statistics;
using MVC.View;
using Class_Diagram;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

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
            ISet<Genre> genres = new HashSet<Genre>();
            var data = GetBodyFromJson<GerneJsonDataModel>();

            if (data.Genre[0] == "all")
            {
                genres = null;
            }else
            {
                for (int i = 0; i < data.Genre.Length; i++)
                {
                    genres.Add(context.Genres.GetByTitle(data.Genre[i]).Result);
                }
            }

            var statistics = context.Users.GetGameWishListStatistics(data.Amount, genres);
            return Json(statistics);
        }
    }
}
