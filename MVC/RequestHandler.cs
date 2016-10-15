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
        private readonly ControllerFactory controllerFartory;
        private readonly HttpListenerContext HttpContext;

        internal RequestHandler(ControllerFactory cFactory, HttpListenerContext context)
        {
            controllerFartory = cFactory;
            HttpContext = context;
        }

        internal ViewObject HandelToView()
        {
            var deCodedRawUrl = WebUtility.UrlDecode(HttpContext.Request.RawUrl);
            var UrlParts = deCodedRawUrl
                .Split('?', '#')
                .First().Split('/')
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .ToList();

            using (var controller = controllerFartory.GetByRawUrl(UrlParts[0]))
            {
                var controllerType = controller.GetType();
                MethodInfo method;
                if(UrlParts.Count > 1)
                {
                    method = controllerType.GetMethods()
                    .FirstOrDefault(mi => mi.Name.ToLower() == $"get{UrlParts[1]}");
                }
                else
                {
                    method = controllerType.GetMethods()
                    .FirstOrDefault(mi => mi.Name.ToLower() == "get");
                }
                var result = method?.Invoke(controller, new object[] { });

                if (result != null && result is ViewObject)
                    return (result as ViewObject);
                else
                    return new RawObjectView(result);
            }

        }
    }
}
