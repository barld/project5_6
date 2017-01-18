using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class NotFoundView : ViewObject
    {
        public NotFoundView()
        {
            StatusCode = 404;
            this.Content = $"<h1>404 not found</h1>";
            ContentType = "text/html; charset=utf-8";
        }

        public NotFoundView(string message): this()
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            this.Content = $"<h1>404 not found</h1><h2>{message}</h2>";
        }
    }
}
