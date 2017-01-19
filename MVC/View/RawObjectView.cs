using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class RawObjectView : ViewObject
    {
        public RawObjectView(object o)
        {
            Content = o?.ToString() ?? "NULL";
            ContentType = "text/html";
        }

    }
}
