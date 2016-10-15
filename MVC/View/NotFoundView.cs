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
        private String message;

        public NotFoundView()
        { }

        public NotFoundView(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            this.message = message;
        }

        public override void Respond(HttpListenerResponse response)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes($"<h1>404 not found</h1><h2>{message}</h2>");

            response.StatusCode = 404;
            response.AppendHeader("Content-Type", "text/html; charset=utf-8");
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
