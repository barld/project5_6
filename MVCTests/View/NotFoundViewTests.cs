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
    public class NotFoundViewTests
    {
        [TestMethod()]
        public void NotFoundViewTestConstructing()
        {
            Assert.IsInstanceOfType(new NotFoundView(), typeof(ViewObject));
        }

        [TestMethod()]
        public void NotFoundViewTestIncludeMessage()
        {
            var message = "testpage.html";
            var view = new NotFoundView(message);
            Assert.IsTrue(view.Content.Contains(message), $"in NotFound.Content: {view.Content} does not contains {message}");
        }

        [TestMethod]
        public void NotFoundViewStatusCode()
        {
            var view = new NotFoundView();
            Assert.AreEqual(view.StatusCode, 404);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotFoundExpectArgumentNullException()
        {
            new NotFoundView(null);
        }
    }
}