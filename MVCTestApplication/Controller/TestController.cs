using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;

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

        public class ExampleData
        {
            public string Message { get; set; }
        }

        public ViewObject PostTest2()
        {
            var data = GetBodyFromJson<ExampleData>();
            data.Message += " Add somthing in the action";
            return Json(data);
        }

        public ViewObject PostTest() => 
            Json(new { Method = "Post", Message = "this is an answer on a POST request" });


        public ViewObject GetExampleData()
        {
            return Json(new {Message= "hello world", Other = 25});
        }
    }
}
