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
    class OrderController : Controller
    {
        Context context = new Context();

        public ViewObject Get()
        {
            return Json(new { Order = true });
        }

        public async void PostSubmit()
        {
            var data = GetBodyFromJson<JsonOrder>();

            int ean_index = -1;
            int amount_index = -1;

            List<OrderLine> orderlineList = new List<OrderLine>();

            foreach (long ean in data.EAN)
            {
                ean_index++;
                foreach (int amount in data.Amounts)
                {
                    amount_index++;
                    if (ean_index == amount_index)
                    {
                        Game game = context.Games.GetByEAN(ean).Result;
                        orderlineList.Add(new OrderLine { Game = game, Amount = amount });
                    }
                }
                amount_index = -1;
            }

            Order order = new Order { OrderLines = orderlineList, OrderDate = DateTime.Now };
            await context.Orders.Insert(order); 
        }
    }
}
