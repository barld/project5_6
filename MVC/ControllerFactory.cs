using MVC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        /// <summary>
        /// insecure if you not realy sure it is a controllertype
        /// </summary>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        internal static ControllerObject CreateController(Type controllerType, Session.Session session)
        {
            ControllerObject o = Activator.CreateInstance(controllerType) as ControllerObject;
            o.Session = session;
            return o;
        }

        internal ControllerObject GetByRawUrl(string name, Session.Session session)
        {
            try
            {
                var cObject = controllers.FirstOrDefault(t => t.Name.ToLower() == $"{name.ToLower()}controller");
                if (cObject != null)
                    return CreateController(cObject, session);
            }
            catch { }

            return new NotFoundController();

        }
    }
}
