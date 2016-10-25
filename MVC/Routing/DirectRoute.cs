using MVC.Controller;
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
        public HttpMethodsEnum Method { get; }
        public string UrlPath { get; }
        public Func<HttpListenerContext, ViewObject> ContextAction { get; }
        public Func<ViewObject> Action { get; }

        public DirectRouteAction(HttpMethodsEnum method, string urlPath, Func<HttpListenerContext,ViewObject> action)
        {
            Method = method;
            UrlPath = urlPath;
            ContextAction = action;
        }

        public DirectRouteAction(HttpMethodsEnum method, string urlPath, Func<ViewObject> action)
        {
            Method = method;
            UrlPath = urlPath;
            Action = action;
        }

        public ViewObject GetView(HttpListenerContext context)
        {
            if (ContextAction != null) return ContextAction(context);
            else return Action();
        }
    }

    public class DirectRouteController : IRoute
    {
        public string UrlPath { get; }
        public Type ControllerType { get; }
        public DirectRouteController(string urlPath, Type controllerType)
        {
            UrlPath = urlPath;
            ControllerType = controllerType;
        }

        public ViewObject GetView(HttpListenerContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class RouteControllers : IRoute
    {
        private readonly ControllerFactory factory;
        public string UrlPath { get; }
        public IEnumerable<Type> ControllersTypes { get; }

        public RouteControllers(string urlPath, IEnumerable<Type> controllerTypes)
        {
            UrlPath = urlPath;
            ControllersTypes = filterControllers(controllerTypes);
            factory = new ControllerFactory(ControllersTypes);
        }

        public RouteControllers(string urlPath, Assembly AssamblyWithControllers)
        {
            UrlPath = urlPath;
            ControllersTypes = filterControllers(AssamblyWithControllers.DefinedTypes);
            factory = new ControllerFactory(ControllersTypes);
        }

        private IEnumerable<Type> filterControllers(IEnumerable<Type> controllerTypes) => controllerTypes.Where(ct => ct.IsSubclassOf(typeof(ControllerObject)));

        public ViewObject GetView(HttpListenerContext context)
        {
            throw new NotImplementedException();
        }
    }

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
            throw new NotImplementedException();
        }
    }

}
