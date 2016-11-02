using MVC;
using MVC.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    class Program
    {
        private static IEnumerable<IRoute> getRoutes()
        {
            return new IRoute[]
            {
                new RouteControllers("/api/", Assembly.GetEntryAssembly()),
                //new DirectRouteAction(HttpMethodsEnum.Get, "/", () => new RawObjectView("hello world")),
                //new DirectRouteAction(HttpMethodsEnum.Get, "/index.html", () => new RawObjectView("hello world")),
                Debugger.IsAttached ? new RouteWebFolder("/", "./../../WebContent/") : new RouteWebFolder("/", "./WebContent/"),
            };
        }

        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                System.Diagnostics.Process.Start("mongod");
                System.Threading.Thread.Sleep(1000);
            }

            var adress = (args?.Length > 0) ? args[0] : "http://localhost:8080/";

            MVCApp app = new MVCApp(adress, getRoutes());
            app.Run();

        }
    }
}
