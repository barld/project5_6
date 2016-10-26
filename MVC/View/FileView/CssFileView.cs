using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class CssFileView : FileView
    {
        public CssFileView(Stream stream) : base(stream)
        {
            contentType = "	text/css";
        }
    }
}
