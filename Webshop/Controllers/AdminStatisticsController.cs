using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using MVC.View;

namespace Webshop.Controllers
{
    class AdminStatisticsController : Controller
    {
        Context context = new Context();

        public ViewObject PostSalesAmountStatistics()
        {
            var data = GetBodyFromJson<JsonDateStatisticData>();

            return Json(context.Orders.GetOrderAmountDataTask(data.TimeSpan, data.BeginDate, data.EndDate));
        }
    }
}
