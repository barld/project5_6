using MVC.Attribute;
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
    public class DirectRouteController : IRoute
    {
        public string UrlPath { get; }
        public Type ControllerType { get; }
        public DirectRouteController(string urlPath, Type controllerType)
        {
            UrlPath = urlPath;
            ControllerType = controllerType;
        }

        public ViewObject GetView(HttpListenerContext context, Session session)
        {
            var restPath = context.Request.RawUrl.Substring(UrlPath.Length);
            var urlParts = restPath
                .Split('/')
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .ToList();
            ControllerObject controller;

            

            var pca = ControllerType.GetCustomAttributes(true).FirstOrDefault(at => at is IFilterControllerAttribute) as IFilterControllerAttribute;
            bool hasPca = pca != null;
            if (hasPca)
                controller = pca.Construct(() => ControllerFactory.CreateController(ControllerType, session, context), session);
            else
                controller = ControllerFactory.CreateController(ControllerType, session, context);

            Func<ViewObject> getView = ControllerFactory.GetView(context, controller, urlParts.Skip(2).ToList());

            if (hasPca)
                return pca.HandeleAction(getView);
            else
                return getView();
            
        }
    }
}
