using MVC.Controller;
using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// should also implement Attribute
    /// </remarks>
    public interface IFilterControllerAttribute
    {
        ControllerObject Construct(Func<ControllerObject> constructFunc, Session session);
        ViewObject HandeleAction(Func<ViewObject> action);
    }

    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AccesDeniedAttribute : System.Attribute, IFilterControllerAttribute
    {
        public AccesDeniedAttribute()
        {

        }

        /// <summary>
        /// don't construct
        /// </summary>
        /// <param name="constructFunc"></param>
        /// <returns></returns>
        public ControllerObject Construct(Func<ControllerObject> constructFunc, Session session) => null;

        public ViewObject HandeleAction(Func<ViewObject> action) => new AccesDeniedView();
    }
}
