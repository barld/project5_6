using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using MVC.View;
using Class_Diagram;

namespace Webshop.Controllers
{
    class StatisticsController : Controller
    {
        Context context = new Context();

        public ViewObject PostSalesAmountStatistics()
        {
            var data = GetBodyFromJson<JsonDateStatisticData>();

            return Json(context.Orders.GetOrderAmountDataTask(data.TimeScale, data.BeginDate, data.EndDate));
        }

        public ViewObject GetTimeScales()
        {
            return Json(Enum.GetNames(typeof(TimeScale)));
        }
    }
}
