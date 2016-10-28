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
        public override void Respond(HttpListenerResponse response)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes("<h1>Acces denied</h1>");

            response.StatusCode = 403;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
