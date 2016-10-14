using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Controller
{
    class TestController : ControllerObject
    {
        public override object Get()
        {
            return "<h1>Hello world</h1>";
        }
    }
}
