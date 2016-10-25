using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DevelopmentTest.Controller
{
    class SecondController : ControllerObject
    {
        public object Get()
        {
            return "<h2>i'm Second</h2>";
        }
    }
}
