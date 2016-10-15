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
        

        internal ControllerObject GetByRawUrl(string name)
        {
            try
            {
                var cObject = Activator.CreateInstance(
                    controllers.FirstOrDefault(t => t.Name.ToLower() == $"{name.ToLower()}controller"));
                if (cObject != null)
                    return cObject as ControllerObject;
            }
            catch { }

            return new NotFoundController();

        }
    }
}
