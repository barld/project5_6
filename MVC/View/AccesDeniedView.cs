using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class AccesDeniedView : ViewObject
    {
        public AccesDeniedView()
        {
            StatusCode = 403;
            Content = "<h1>Acces denied</h1>";
        }

    }
}
