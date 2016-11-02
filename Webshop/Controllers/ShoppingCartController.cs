using Class_Diagram.ShoppingCart;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Controllers
{

    /// <summary>
    /// used session key:
    ///     - shoppingCart
    /// </summary>
    public class ShoppingCartController : MVC.Controller.Controller
    {
        #region logic
        const string shoppingCartKey = "shoppingCart";

        Cart currentShoppingCart
        {
            get
            {
                if (!Session.Data.ContainsKey(shoppingCartKey))
                    Session.Data.Add(shoppingCartKey, new Cart());
                return Session.Data[shoppingCartKey] as Cart;
            }
            set
            {
                if (!Session.Data.ContainsKey(shoppingCartKey))
                    Session.Data.Add(shoppingCartKey, value);
                Session.Data[shoppingCartKey] = value;
            }
        }

        #endregion

        public ViewObject Get()
        {
            return Json(currentShoppingCart);
        }

        public ViewObject Put()
        {
            try
            {
                currentShoppingCart = GetBodyFromJson<Cart>();
                return Json(new Models.ActionResultViewModel { Succes = true, Message = "shoppingcart succesvol updated" });
            }
            catch
            {
                return Json(new Models.ActionResultViewModel { Succes = false, Message = "something went wrong" });
            }
        }

        public ViewObject Delete()
        {
            currentShoppingCart = new Cart();
            return Json(new Models.ActionResultViewModel { Succes = true, Message = "shoppingcart deleted" });
        }

    }
}
