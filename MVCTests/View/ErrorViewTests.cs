using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View.Tests
{
    [TestClass()]
    public class ErrorViewTests
    {
        [TestMethod()]
        public void ErrorViewTestConstructing()
        {
            Assert.IsInstanceOfType(new ErrorView(), typeof(ViewObject));
        }

        [TestMethod]
        public void ErrorViewStatusCodeTest()
        {
            var view = new ErrorView();
            Assert.AreEqual(view.StatusCode, 500);
        }
    }
}