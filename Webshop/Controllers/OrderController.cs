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
            var auth = new AuthModule(Session, this.context);
            var shoppingCartModule = new ShoppingCartLogic(Session, this.context);

            User customer = auth.CurrentUser;
            int newOrderNumber = context.Orders.GetLatestOrderNumber() + 1;
            Address deliveryAddress = new Address { City = data.DeliveryCity, Country = data.DeliveryCountry, Housenumber = data.DeliveryHousenumber, PostalCode = data.DeliveryPostalCode, Streetname = data.DeliveryStreetname };
            Address billingAddress = new Address { City = data.BillingCity, Country = data.BillingCountry, Housenumber = data.BillingHousenumber, PostalCode = data.BillingPostalCode, Streetname = data.DeliveryStreetname };
            Order order = new Order
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                OrderLines = shoppingCartModule.CurrentShoppingCart.CartLines.Select(cl => cl.ToOrderLine()),
                OrderDate = DateTime.Now,
                Customer_Id = customer._id,
                OrderNumber = newOrderNumber,
                DeliveryAddress = deliveryAddress,
                BillingAddress = billingAddress
            };
            await context.Orders.Insert(order);
        }
    }
}
