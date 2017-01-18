using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC;
using MVC.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests
{
    [TestClass()]
    public class MVCAppTests
    {
        [TestMethod()]
        public void MVCAppTest()
        {
            var app = new MVCApp("http://127.0.0.1", new IRoute[] { });
        }
    }
}