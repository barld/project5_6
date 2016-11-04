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
            User customer = context.Users.GetByEmail(data.Email).Result;
            int newOrderNumber = context.Orders.GetLatestOrderNumber() + 1;
            Address deliveryAddress = new Address { City=data.DeliveryCity, Country=data.DeliveryCountry, Housenumber=data.DeliveryHousenumber, PostalCode=data.DeliveryPostalCode, Streetname=data.DeliveryStreetname };
            Address billingAddress = new Address { City=data.BillingCity, Country=data.BillingCountry, Housenumber=data.BillingHousenumber, PostalCode=data.BillingPostalCode, Streetname=data.DeliveryStreetname };
            Order order = new Order { OrderLines = orderlineList, OrderDate = DateTime.Now, Customer=customer, OrderNumber=newOrderNumber, DeliveryAddress=deliveryAddress, BillingAddress=billingAddress};
            await context.Orders.Insert(order); 
        }
    }
}
