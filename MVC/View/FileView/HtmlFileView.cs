using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    class HtmlFileView : FileView
    {
        public HtmlFileView(Stream stream) : base(stream)
        {
            contentType = "text/html";
        }
    }
}
