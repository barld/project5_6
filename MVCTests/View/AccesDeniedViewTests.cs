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
    public class AccesDeniedViewTests
    {
        [TestMethod()]
        public void AccesDeniedViewTest_StatusCode()
        {
            var view = new AccesDeniedView();
            Assert.AreEqual(view.StatusCode, 403);
        }

        [TestMethod()]
        public void AccesDeniedViewTest_content()
        {
            var view = new AccesDeniedView();
            Assert.IsTrue(view.Content.ToLower().Contains("acces denied"));
        }
    }
}