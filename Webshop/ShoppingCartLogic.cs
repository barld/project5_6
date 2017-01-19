using Class_Diagram.ShoppingCart;
using DataModels;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    /// <summary>
    /// used session key:
    ///     - shoppingCart
    /// </summary>
    public class ShoppingCartLogic
    {
        readonly Session session;
        const string shoppingCartKey = "shoppingCart";
        readonly IContext context;

        public ShoppingCartLogic(Session session, IContext context)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (context == null) throw new ArgumentNullException(nameof(context));

            this.session = session;
            this.context = context;
        }       

        public Cart currentShoppingCart
        {
            get
            {
                if (!session.Data.ContainsKey(shoppingCartKey))
                    session.Data.Add(shoppingCartKey, new Cart());
                return session.Data[shoppingCartKey] as Cart;
            }
            private set
            {
                if (!session.Data.ContainsKey(shoppingCartKey))
                    session.Data.Add(shoppingCartKey, value);
                session.Data[shoppingCartKey] = value;
            }
        }

        public void AddToCart(Game game)
        {
            if (currentShoppingCart.CartLines.Count(g => g.Product.EAN == game.EAN) == 0)
            {
                currentShoppingCart.CartLines.Add(new CartLine { Amount = 1, Product = context.Games.GetByEAN(game.EAN).Result });
            }
            else
            {
                currentShoppingCart.CartLines.First(c => c.Product.EAN == game.EAN).Amount++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        public void ReplaceShoppingCart(Cart cart)
        {
            this.Clear();  
            cart.CartLines.ForEach(cl => 
            {
                cl.Product = context.Games.GetByEAN(cl.Product.EAN).Result;
                cl.Amount = cl.Amount < 1000 ? cl.Amount : 1000; // max 1000 items will
            });
            cart.CartLines = cart.CartLines.Where(cl => cl.Amount > 0)
                .GroupBy(cl => cl.Product.EAN).Select(group => { group.First().Amount = group.Sum(cl => cl.Amount); return group.First(); })//Group the same items
                .ToList();
            currentShoppingCart = cart;
        }

        public void Clear()
        {
            currentShoppingCart = new Cart();
        }

        public void RemoveOneItem(Game game)
        {
            currentShoppingCart.CartLines.ForEach(cl => { if (cl.Product.EAN == game.EAN) cl.Amount--; });
            currentShoppingCart.CartLines = currentShoppingCart.CartLines.Where(cl => cl.Amount > 0).ToList();
        }
    }
}
