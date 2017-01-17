using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class ErrorView : ViewObject
    {
        public ErrorView()
        {
            Content = "{Succes: false}";
            StatusCode = 500;
            ContentType = "application/json";
        }

    }
}
