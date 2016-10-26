using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public DirectRouteAction(HttpMethodsEnum method, string urlPath, Func<HttpListenerContext, ViewObject> action)
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
}
