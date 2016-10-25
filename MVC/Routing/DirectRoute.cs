using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Routing
{
    public class DirectRouteAction : IRoute
    {
        public DirectRouteAction(HttpMethodsEnum method, string path, Func<HttpListenerContext,ViewObject> action)
        {

        }

        public DirectRouteAction(HttpMethodsEnum method, string path, Func<ViewObject> action)
        {

        }
    }

    public class DirectRouteController : IRoute
    {
        public DirectRouteController(string UrlPath, Type controllerType)
        {

        }
    }

    public class RouteControllers : IRoute
    {
        public RouteControllers(string UrlPath, IEnumerable<Type> controllerTypes)
        {

        }

        public RouteControllers(string UrlPath, Assembly AssamblyWithControllers)
        {

        }
    }

    public class RouteWebFolder : IRoute
    {
        public RouteWebFolder(string UrlPath, string folder)
        {

        }
    }

}
