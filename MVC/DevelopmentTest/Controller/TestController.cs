using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;

namespace MVC.DevelopmentTest.Controller
{
    class TestController : ControllerObject
    {
        public override object Get()
        {
            return "<h1>Hello world</h1>";
        }

        public string GetTest()
        {
            return "this is a test";
        }

        public ViewObject GetExampleData()
        {
            return new JsonDataView(new {Message= "hello world", Other = 25});
        }
    }
}
