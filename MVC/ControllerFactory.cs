using MVC.Controller;
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
    internal class ControllerFactory
    {
        private readonly IEnumerable<Type> controllers;

        internal ControllerFactory(Assembly assembly)
        {
            this.controllers = getControllers(assembly);
        }
        internal ControllerFactory(IEnumerable<Assembly> assamblys)
        {
            this.controllers = assamblys.SelectMany(getControllers);
        }
        internal ControllerFactory(IEnumerable<Type> controllers)
        {
            this.controllers = filterControllers(controllers);
        }
        private IEnumerable<Type> getControllers(Assembly assembly) => filterControllers(assembly.DefinedTypes);

        private IEnumerable<Type> filterControllers(IEnumerable<Type> types) => 
            types.Where(ti => ti.IsSubclassOf(typeof(ControllerObject)));

        internal static Func<ViewObject> GetView(HttpListenerContext context, ControllerObject controller, List<string> urlParts)
        {
            return () =>
            {
                controller._requestContext = context;
                var HttpMethod = context.Request.HttpMethod.ToLower();

                var controllerType = controller.GetType();


                MethodInfo method;
                if (urlParts.Count > 0)
                {
                    method = controllerType.GetMethods()
                    .FirstOrDefault(mi => mi.Name.ToLower() == $"{HttpMethod}{urlParts[0]}");
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
            };
        }
        
        /// <summary>
        /// insecure if you not realy sure it is a controllertype
        /// </summary>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        internal static ControllerObject CreateController(Type controllerType, Session session, HttpListenerContext context)
        {
            int indexQuestion = context.Request.RawUrl.IndexOf('?');
            int indexHastag = context.Request.RawUrl.IndexOf('#');
            string parameterpart = string.Empty;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (indexQuestion != -1)
            {
                if (indexQuestion > indexHastag)
                {
                    parameterpart = context.Request.RawUrl.Split('?')[1];
                }
                else
                {
                    parameterpart = context.Request.RawUrl.Split('?', '#')[1];
                }

                foreach (string part in parameterpart.Split('&'))
                {
                    parameters.Add( WebUtility.UrlDecode(part.Split('=')[0]), WebUtility.UrlDecode(part.Split('=')[1]));
                }
            }
            



            ControllerObject o = Activator.CreateInstance(controllerType) as ControllerObject;
            o.Session = session;
            o.AfterConstruct();
            o.Parameters = parameters;
            return o;
        }

        internal Type GetByRawUrl(string name, Session session)
        {
            try
            {
                var cObject = controllers.FirstOrDefault(t => t.Name.ToLower() == $"{name.ToLower()}controller");
                if (cObject != null)
                    return cObject;
            }
            catch { }

            return typeof(NotFoundController);

        }
    }
}
