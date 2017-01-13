using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MVC.Helpers;
using System.Linq;

namespace MVCTests.Helpers
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void PickWhereType_Test_EnoughItems()
        {
            var exmapleList = new List<object> { new List<int>(), new List<string>(), new List<string>() };
            Assert.AreEqual(exmapleList.PickWhereType<object, List<string>>().Count(), 2);
        }

        [TestMethod]
        public void PickWhereType_Test_notexisting()
        {
            var exmapleList = new List<object> { new List<int>(), new List<string>(), new List<string>() };
            Assert.AreEqual(exmapleList.PickWhereType<object, string>().Count(), 0);
        }
    }
}
