using Class_Diagram.ShoppingCart;
using DataModels;
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
        private Context context;

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

        public override void AfterConstruct()
        {
            this.context = new Context();
        }

        public ViewObject Get()
        {
            return Json(currentShoppingCart);
        }

        public ViewObject PostAdd()
        {
            var game = this.GetBodyFromJson<Game>();
            if(currentShoppingCart.CartLines.Count(g => g.Product.EAN == game.EAN) == 0)
            {
                currentShoppingCart.CartLines.Add(new CartLine { Amount = 1, Product = context.Games.GetByEAN(game.EAN).Result });
            }
            else
            {
                currentShoppingCart.CartLines.First(c => c.Product.EAN == game.EAN).Amount++;
            }
            return Json(new Models.ActionResultViewModel { Success = true });
        }

        public ViewObject Put()
        {
            try
            {
                currentShoppingCart = GetBodyFromJson<Cart>();
                currentShoppingCart.CartLines.ForEach(cl => cl.Product = context.Games.GetByEAN(cl.Product.EAN).Result);
                currentShoppingCart.CartLines = currentShoppingCart.CartLines.Where(cl => cl.Amount > 0).ToList();
                return Json(new Models.ActionResultViewModel { Success = true, Message = "shoppingcart succesvol updated" });
            }
            catch
            {
                return Json(new Models.ActionResultViewModel { Success = false, Message = "something went wrong" });
            }
        }

        public ViewObject Delete()
        {
            currentShoppingCart = new Cart();
            return Json(new Models.ActionResultViewModel { Success = true, Message = "shoppingcart deleted" });
        }

        public ViewObject DeleteRemove()
        {
            var game = GetBodyFromJson<Game>();
            currentShoppingCart.CartLines.ForEach(cl => { if (cl.Product.EAN == game.EAN) cl.Amount--; });
            currentShoppingCart.CartLines = currentShoppingCart.CartLines.Where(cl => cl.Amount > 0).ToList();
            return Json(new Models.ActionResultViewModel { Success = true, Message = "item deleted" });
        }

    }
}
