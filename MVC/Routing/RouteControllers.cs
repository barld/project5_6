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

        public ViewObject GetView(HttpListenerContext context, Session session)
        {
            var restPath = context.Request.RawUrl.Substring(UrlPath.Length).Split('?', '#')[0];
            var urlParts = restPath
                .Split('/')
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .ToList();
            ControllerObject controller;
            var ControllerType = factory.GetByRawUrl(urlParts[0], session);


            var pca = ControllerType.GetCustomAttributes(true).FirstOrDefault(at => at is IFilterControllerAttribute) as IFilterControllerAttribute;
            bool hasPca = pca != null;
            if (hasPca)
                controller = pca.Construct(() => ControllerFactory.CreateController(ControllerType, session, context), session);
            else
                controller = ControllerFactory.CreateController(ControllerType, session, context);

            Func<ViewObject> getView = ControllerFactory.GetView(context, controller, urlParts.Skip(1).ToList());

            if (hasPca)
                return pca.HandeleAction(getView);
            else
                return getView();

        }
    }
}
