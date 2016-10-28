using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;
using MVC.Attribute;

namespace MVC.DevelopmentTest.Controller
{
    [AccesDenied]
    class TestController : MVC.Controller.Controller
    {
        public object Get()
        {
            return "<h1>Hello world</h1>";
        }

        public ViewObject GetSession()
        {
            int count=1;
            if(this.Session.Data.ContainsKey("count"))
            {
                count += (int)this.Session.Data["count"];
                this.Session.Data["count"] = count;

            }
            else
            {
                this.Session.Data.Add("count", count);
            }
            return new RawObjectView(count);
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
