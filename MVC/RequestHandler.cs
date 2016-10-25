using MVC.Routing;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    internal class RequestHandler
    {
        private readonly IEnumerable<IRoute> routes;
        private readonly HttpListenerContext HttpContext;

        internal RequestHandler(IEnumerable<IRoute> routes, HttpListenerContext context)
        {
            this.routes = routes;
            HttpContext = context;
        }

        internal ViewObject HandelToView()
        {
            var deCodedRawUrl = WebUtility.UrlDecode(HttpContext.Request.RawUrl);
            var path = deCodedRawUrl
                .Split('?', '#').First();
            var urlParts = path
                .Split('/')
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .ToList();

            var view = routes.Where(r => r is DirectRouteAction).Select(r => r as DirectRouteAction).FirstOrDefault(dr => dr.UrlPath == path)?.GetView(HttpContext);

            if (view != null)
                return view;
            else
                return new NotFoundView("action not found");

            //using (var controller = controllerFartory.GetByRawUrl(urlParts[0]))
            //{
            //    controller._requestContext = HttpContext;
            //    var HttpMethod = HttpContext.Request.HttpMethod.ToLower();

            //    var controllerType = controller.GetType();
            //    MethodInfo method;
            //    if(urlParts.Count > 1)
            //    {
            //        method = controllerType.GetMethods()
            //        .FirstOrDefault(mi => mi.Name.ToLower() == $"{HttpMethod}{urlParts[1]}");
            //    }
            //    else
            //    {
            //        method = controllerType.GetMethods()
            //        .FirstOrDefault(mi => mi.Name.ToLower() == $"{HttpMethod}");
            //    }
            //    var result = method?.Invoke(controller, new object[] { });

            //    if (result != null && result is ViewObject)
            //        return (result as ViewObject);
            //    else if(method == null)
            //        return new NotFoundView("action not found");
            //    else
            //        return new RawObjectView(result);
            //}

        }
    }
}
