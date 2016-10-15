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
        object o;
        public RawObjectView(object o)
        {
            this.o = o;
        }

        public override void Respond(HttpListenerResponse response)
        {
            if (o == null)
                o = "NULL";
            var buffer = System.Text.Encoding.UTF8.GetBytes(o.ToString());

            response.AppendHeader("Content-Type", "text/html; charset=utf-8");
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
