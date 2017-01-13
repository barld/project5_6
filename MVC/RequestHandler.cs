using MVC.Helpers;
using MVC.Routing;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

        internal ViewObject HandelToView(Session session)
        {
            try
            {
                var deCodedRawUrl = WebUtility.UrlDecode(HttpContext.Request.RawUrl);
                var path = Regex.Match(deCodedRawUrl, @"^([^\?#]+)").Value;
                var urlParts = path
                    .Split('/')
                    .Where(s => !String.IsNullOrWhiteSpace(s))
                    .Select(s => s.ToLower())
                    .ToList();

                // DirectRouteAction
                var view = routes.PickWhereType<IRoute, DirectRouteAction>()
                    .FirstOrDefault(dr => HttpMethods.fromHttpMethodsEnum(dr.Method) == HttpContext.Request.HttpMethod && dr.UrlPath == path)?.GetView(HttpContext);

                if (view != null)
                    return view;

                //DirectRouteController
                view = routes.PickWhereType<IRoute, DirectRouteController>()
                    .FirstOrDefault(drc => path.StartsWith(drc.UrlPath))?.GetView(HttpContext, session);
                if (view != null)
                    return view;

                view = routes.PickWhereType<IRoute, RouteControllers>()
                    .FirstOrDefault(rc => path.StartsWith(rc.UrlPath))?.GetView(HttpContext, session);

                if (view != null)
                    return view;

                view =
                    routes.PickWhereType<IRoute, RouteWebFolder>()
                        .FirstOrDefault(rw => path.StartsWith(rw.UrlPath))?.GetView(HttpContext);

                if (view != null)
                    return view;

                else
                    return new NotFoundView("action not found");
            }
            catch(Exception e)
            {
                return new ErrorView();
            }
            

        }
    }
}
