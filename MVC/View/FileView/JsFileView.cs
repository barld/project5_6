using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class JsFileView : FileView
    {
        public JsFileView(Stream stream) : base(stream)
        {
            contentType = "application/x-javascript";
        }
    }
}
