using MVC.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Routing
{
    public class RouteWebFolder : IRoute
    {
        public string UrlPath { get; }
        public string Folder { get; }
        public RouteWebFolder(string urlPath, string folder)
        {
            UrlPath = urlPath;
            Folder = folder;
        }

        public ViewObject GetView(HttpListenerContext context)
        {
            var restPath = context.Request.RawUrl.Substring(UrlPath.Length).Split('?', '#').First();
            
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Folder, restPath);
            Console.WriteLine(file);

            if(Directory.Exists(file) && File.GetAttributes(file).HasFlag(FileAttributes.Directory))
                file += "index.html";
            if(File.Exists(file))
            {
                var s = File.OpenRead(file);
                switch (Path.GetExtension(file).ToLower())
                {
                    case ".css":
                        return new CssFileView(s);
                    case ".html":
                    case ".xhtml":
                    case ".shtml":
                    case ".htm":
                        return new HtmlFileView(s);
                    case ".js":
                        return new JsFileView(s);
                    case ".jpg":
                        return new JpegFileView(s);
                    case ".png":
                        return new PngFileView(s);
                    default:
                        return new FileView(s);

                }
            }
            return new NotFoundView("file not found");
        }
    }
}
