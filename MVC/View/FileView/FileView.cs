using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class FileView : ViewObject
    {
        public FileView(Stream stream)
        {
            this.stream = stream;
        }

        protected string contentType = "application/file";
        private readonly Stream stream;

        public override void Respond(HttpListenerResponse response)
        {

            response.ContentType = contentType;
            response.ContentLength64 = stream.Length;

            stream.CopyTo(response.OutputStream);
            response.OutputStream.Close();

            stream.Close();
            stream.Dispose();
        }
    }
}
