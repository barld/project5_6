using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webshop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopTests.ContextMock;
using DataModels;
using Class_Diagram.ShoppingCart;

namespace Webshop.Tests
{
    [TestClass()]
    public class ShoppingCartLogicTests
    {
        private MockContext context;
        private ShoppingCartLogic logic;

        [TestMethod()]
        public void ShoppingCartLogicTest()
        {
            var logic = new ShoppingCartLogic(new MVC.Session(), new MockContext());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShoppingCartLogicNullCheck()
        {
            new ShoppingCartLogic(null, null);
        }

        [TestInitialize]
        public void Setup()
        {
            context = new MockContext(true);
            logic = new ShoppingCartLogic(new MVC.Session(), context);
        }

        [TestMethod()]
        public void AddToCartTestOne()
        {
            var game1 = context.Games.GetAll().Result.First();
            logic.AddToCart(game1);

            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.Count, "thereshould be one cartline on the shoppingCart");
            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.First().Amount, "the item cartline have a amount of 1");
        }

        [TestMethod()]
        public void AddToCartTestTwoSame()
        {
            var game1 = context.Games.GetAll().Result.First();

            logic.AddToCart(game1);
            logic.AddToCart(game1);

            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.Count, "thereshould be one cartline on the shoppingCart");
            Assert.AreEqual(2, logic.CurrentShoppingCart.CartLines.First().Amount, "the item cartline have a amount of 2");
        }

        [TestMethod()]
        public void AddToCartTest50Same()
        {
            var context = new MockContext(true);
            var logic = new ShoppingCartLogic(new MVC.Session(), context);
            var game1 = context.Games.GetAll().Result.First();

            for(int i=0;i<50;i++)
                logic.AddToCart(game1);

            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.Count, "thereshould be one cartline on the shoppingCart");
            Assert.AreEqual(50, logic.CurrentShoppingCart.CartLines.First().Amount, "the item cartline have a amount of 50");
        }

        [TestMethod()]
        public void AddToCartTestTwoDifferent()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            var game2 = context.Games.GetAll().Result.ElementAt(1);

            logic.AddToCart(game1);
            logic.AddToCart(game2);

            Assert.AreEqual(2, logic.CurrentShoppingCart.CartLines.Count, "thereshould be two cartline on the shoppingCart");

            //game1
            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.First(cl => cl.Product.EAN == game1.EAN).Amount, "the item cartline have a amount of 1");

            //game2
            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.First(cl => cl.Product.EAN == game2.EAN).Amount, "the item cartline have a amount of 1");
        }

        [TestMethod()]
        public void AddToCartTestTwoTimes50Different()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            var game2 = context.Games.GetAll().Result.ElementAt(1);

            for(int i = 0; i < 50; i++)
            {
                logic.AddToCart(game1);
                logic.AddToCart(game2);
            }
            

            Assert.AreEqual(2, logic.CurrentShoppingCart.CartLines.Count, "thereshould be two cartline on the shoppingCart");

            //game1
            Assert.AreEqual(50, logic.CurrentShoppingCart.CartLines.First(cl => cl.Product.EAN == game1.EAN).Amount, "the item cartline have a amount of 50");

            //game2
            Assert.AreEqual(50, logic.CurrentShoppingCart.CartLines.First(cl => cl.Product.EAN == game2.EAN).Amount, "the item cartline have a amount of 50");
        }

        [TestMethod()]
        public void ReplaceShoppingCartTestWithEmptyCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);

            logic.AddToCart(game1);
            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart();
            logic.ReplaceShoppingCart(newCart);

            Assert.AreNotEqual(oldCart, logic.CurrentShoppingCart);
            Assert.AreEqual(newCart.CartLines.Count, logic.CurrentShoppingCart.CartLines.Count);       
        }

        /// <summary>
        /// add cartline with negative and zero amount
        /// </summary>
        [TestMethod()]
        public void ReplaceShoppingCartTestWithIligalCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            var game2 = context.Games.GetAll().Result.ElementAt(1);

            logic.AddToCart(game1);
            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart {
                CartLines =
                {
                    new CartLine{Amount = 0, Product = game1},
                    new CartLine{Amount = -2, Product = game2}
                }
            };
            logic.ReplaceShoppingCart(newCart);

            Assert.AreNotEqual(oldCart, logic.CurrentShoppingCart);
            Assert.AreEqual(0, logic.CurrentShoppingCart.CartLines.Count);
        }

        [TestMethod()]
        public void ReplaceShoppingCartTestWithSameItemsCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            var game2 = context.Games.GetAll().Result.ElementAt(1);

            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart
            {
                CartLines =
                {
                    new CartLine{Amount = 1, Product = game1},
                    new CartLine{Amount = 2, Product = game2},
                    new CartLine{Amount = 3, Product = game2}
                }
            };
            logic.ReplaceShoppingCart(newCart);
            Assert.AreEqual(2, logic.CurrentShoppingCart.CartLines.Count);
        }

        [TestMethod()]
        public void ReplaceShoppingCartTestWithToBigAmountCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);

            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart
            {
                CartLines =
                {
                    new CartLine{Amount = 1000*100, Product = game1},
                }
            };
            logic.ReplaceShoppingCart(newCart);
            Assert.AreEqual(100, logic.CurrentShoppingCart.CartLines.First().Amount);
        }

        [TestMethod()]
        public void ReplaceShoppingCartTestWithTo101AmountCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);

            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart
            {
                CartLines =
                {
                    new CartLine{Amount = 101, Product = game1},
                }
            };
            logic.ReplaceShoppingCart(newCart);
            Assert.AreEqual(100, logic.CurrentShoppingCart.CartLines.First().Amount);
        }

        [TestMethod()]
        public void ReplaceShoppingCartTestWithTo100AmountCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);

            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart
            {
                CartLines =
                {
                    new CartLine{Amount = 100, Product = game1},
                }
            };
            logic.ReplaceShoppingCart(newCart);
            Assert.AreEqual(100, logic.CurrentShoppingCart.CartLines.First().Amount);
        }

        [TestMethod()]
        public void ReplaceShoppingCartTestWithTo99AmountCart()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);

            var oldCart = logic.CurrentShoppingCart;
            var newCart = new Cart
            {
                CartLines =
                {
                    new CartLine{Amount = 99, Product = game1},
                }
            };
            logic.ReplaceShoppingCart(newCart);
            Assert.AreEqual(99, logic.CurrentShoppingCart.CartLines.First().Amount);
        }


        [TestMethod()]
        public void ClearTest()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            logic.AddToCart(game1);
            logic.Clear();

            Assert.AreEqual(0, logic.CurrentShoppingCart.CartLines.Count);
            Assert.AreEqual(0, logic.CurrentShoppingCart.TotalPrice);
        }

        [TestMethod()]
        public void RemoveOneItemTest_RemoveNotExistingGame()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);

            logic.RemoveOneItem(game1);
            Assert.AreEqual(0, logic.CurrentShoppingCart.TotalPrice);
            Assert.AreEqual(0, logic.CurrentShoppingCart.CartLines.Count);
        }

        [TestMethod()]
        public void RemoveOneItemTest_RemoveOneExistingGame()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            logic.AddToCart(game1);

            logic.RemoveOneItem(game1);
            Assert.AreEqual(0, logic.CurrentShoppingCart.TotalPrice);
            Assert.AreEqual(0, logic.CurrentShoppingCart.CartLines.Count);
        }

        [TestMethod()]
        public void RemoveOneItemTest_RemoveOneOfTwoExistingGame()
        {
            var game1 = context.Games.GetAll().Result.ElementAt(0);
            logic.AddToCart(game1);
            logic.AddToCart(game1);

            logic.RemoveOneItem(game1);
            Assert.AreEqual(1, logic.CurrentShoppingCart.CartLines.Count);
        }
    }
}