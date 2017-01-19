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
    public class RawObjectViewTests
    {
        [TestMethod()]
        public void RawObjectViewTest()
        {
            Assert.IsInstanceOfType(new RawObjectView(null), typeof(ViewObject));
        }

        [TestMethod]
        public void RawObjectViewContent()
        {
            var content = 25.23;
            var view = new RawObjectView(content);

            Assert.AreEqual(view.Content, content.ToString());

        }

        [TestMethod]
        public void RawObjectViewContentNull()
        {
            object content = null;
            var view = new RawObjectView(content);

            Assert.AreEqual(view.Content, "NULL");

        }

        [TestMethod]
        public void RawObjectViewTestStatusCode()
        {
            var view = new RawObjectView("");
            Assert.AreEqual(view.StatusCode, 200);
        }

    }
}