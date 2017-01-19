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

    public class ShoppingCartController : MVC.Controller.Controller
    {
        private Context context;
        private ShoppingCartLogic shoppingCartLogic;

        public override void AfterConstruct()
        {
            this.context = new Context();
            this.shoppingCartLogic = new ShoppingCartLogic(this.Session, this.context);

        }

        /// <summary>
        /// get the complete shoppingcart for the current user
        /// </summary>
        /// <returns></returns>
        public ViewObject Get()
        {
            return Json(shoppingCartLogic.currentShoppingCart);
        }

        /// <summary>
        /// Add one game to the shoppingcart
        /// </summary>
        /// <returns></returns>
        public ViewObject PostAdd()
        {
            var game = this.GetBodyFromJson<Game>();
            shoppingCartLogic.AddToCart(game);
            return Json(new Models.ActionResultViewModel { Success = true });
        }

        /// <summary>
        /// Update the complete shoppingcart
        /// </summary>
        /// <returns></returns>
        public ViewObject Put()
        {
            try
            {
                var cart = GetBodyFromJson<Cart>();
                shoppingCartLogic.ReplaceShoppingCart(cart);                
                return Json(new Models.ActionResultViewModel { Success = true, Message = "shoppingcart succesvol updated" });
            }
            catch
            {
                return Json(new Models.ActionResultViewModel { Success = false, Message = "something went wrong" });
            }
        }

        /// <summary>
        /// clear the shopping cart
        /// </summary>
        /// <returns></returns>
        public ViewObject Delete()
        {
            shoppingCartLogic.Clear();
            return Json(new Models.ActionResultViewModel { Success = true, Message = "shoppingcart deleted" });
        }

        /// <summary>
        /// remove the the game by a amount of 1 item by multiple items the amount will be amount-- else the whole game is deleted
        /// </summary>
        /// <returns></returns>
        public ViewObject DeleteRemove()
        {
            var game = GetBodyFromJson<Game>();
            
            return Json(new Models.ActionResultViewModel { Success = true, Message = "item deleted" });
        }

    }
}
