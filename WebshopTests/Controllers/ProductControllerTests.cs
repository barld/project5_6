using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webshop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        ProductController productController = new ProductController();

        [TestMethod()]
        public void GetTest()
        {
            productController.Get();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            productController.GetAll();
        }

        [TestMethod()]
        public void PostSearchTest()
        {
            productController.PostSearch();
        }

        [TestMethod()]
        public void GetGamebyPlatformTest()
        {
            productController.GetGamebyPlatform();
        }

        [TestMethod()]
        public void PostGameTest()
        {
            productController.PostGame();
        }
    }
}