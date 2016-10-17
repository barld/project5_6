using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;
using MVC.Controller;

namespace MVC.DevelopmentTest.Controller
{
    class TestController : MVC.Controller.Controller
    {
        public object Get()
        {
            return "<h1>Hello world</h1>";
        }

        public string GetTest()
        {
            return "this is a test";
        }

        public ViewObject PostTest() => Json(new { Method = "Post", Message = "this is an answer on a POST request" });


        public ViewObject GetExampleData()
        {
            return Json(new {Message= "hello world", Other = 25});
        }
    }
}
