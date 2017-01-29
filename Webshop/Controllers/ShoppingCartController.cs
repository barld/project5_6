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
        const string shoppingCartKey = "shoppingCart";
        private Context context;
        private ShoppingCartLogic logic;
        

        public override void AfterConstruct()
        {
            this.context = new Context();
            logic = new ShoppingCartLogic(Session, context);
        }

        public ViewObject Get()
        {
            return Json(logic.CurrentShoppingCart);
        }

        public ViewObject PostAdd()
        {
            var game = this.GetBodyFromJson<Game>();
            logic.AddToCart(game);
            return Json(new Models.ActionResultViewModel { Success = true });
        }

        public ViewObject Put()
        {
            try
            {
                var cart = GetBodyFromJson<Cart>();
                logic.ReplaceShoppingCart(cart);
                return Json(new Models.ActionResultViewModel { Success = true, Message = "shoppingcart succesvol updated" });
            }
            catch
            {
                return Json(new Models.ActionResultViewModel { Success = false, Message = "something went wrong" });
            }
        }

        public ViewObject DeleteAll()
        {
            logic.Clear();
            return Json(new Models.ActionResultViewModel { Success = true, Message = "shoppingcart deleted" });
        }

        public ViewObject DeleteRemove()
        {
            var game = GetBodyFromJson<Game>();
            logic.RemoveOneItem(game);
            return Json(new Models.ActionResultViewModel { Success = true, Message = "item deleted" });
        }

    }
}
