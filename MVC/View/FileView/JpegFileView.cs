using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    class JpegFileView : FileView
    {
        public JpegFileView(Stream stream) : base(stream)
        {
            contentType = "image/jpeg";
        }
    }
}
