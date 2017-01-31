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
            var view = new JsonDataView(new { test = true});
            Assert.AreEqual(view.StatusCode, 200);
        }

        [TestMethod()]
        public void JsonDataViewTestContent()
        {
            var view = new JsonDataView(new { test = "test" });
            Assert.IsTrue(!String.IsNullOrEmpty(view.Content));
        }

        [TestMethod()]
        public void JsonDataViewTestContentType()
        {
            var view = new JsonDataView(new { test = "test" });
            Assert.IsTrue(view.ContentType.Contains("application/json"));
        }
    }
}