using MVC;
using MVC.View;
using MVC.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MVCTestApplication
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
                new RouteWebFolder("/", "./WebContent/"),
            };
        }

        static void Main(string[] args)
        {
            var adress = (args?.Length > 0) ? args[0] : "http://localhost:8080/";

            MVCApp app = new MVCApp(adress, getRoutes());
            app.Run();

        }
    }
}
