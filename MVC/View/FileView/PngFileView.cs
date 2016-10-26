using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public class PngFileView : FileView
    {
        public PngFileView(Stream stream) : base(stream)
        {
            contentType = "image/png";
        }

    }
}
