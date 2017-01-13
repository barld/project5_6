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
    public class JsonDataViewTests
    {
        [TestMethod()]
        public void JsonDataViewTestStatusCode()
        {
            var view = new JsonDataView(new { });
            Assert.AreEqual(view.StatusCode, 200);
        }
    }
}