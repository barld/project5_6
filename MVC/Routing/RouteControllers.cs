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

        public ViewObject GetView(HttpListenerContext context)
        {
            var restPath = context.Request.RawUrl.Substring(UrlPath.Length);
            var urlParts = restPath
                .Split('/')
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .ToList();

            using (var controller = factory.GetByRawUrl(urlParts[0]))
            {
                controller._requestContext = context;
                var HttpMethod = context.Request.HttpMethod.ToLower();

                var controllerType = controller.GetType();
                MethodInfo method;
                if (urlParts.Count > 1)
                {
                    method = controllerType.GetMethods()
                    .FirstOrDefault(mi => mi.Name.ToLower() == $"{HttpMethod}{urlParts[1]}");
                }
                else
                {
                    method = controllerType.GetMethods()
                    .FirstOrDefault(mi => mi.Name.ToLower() == $"{HttpMethod}");
                }
                var result = method?.Invoke(controller, new object[] { });

                if (result != null && result is ViewObject)
                    return (result as ViewObject);
                else if (method == null)
                    return new NotFoundView("action not found");
                else
                    return new RawObjectView(result);
            }
        }
    }
}
