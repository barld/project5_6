using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Attribute
{
    /// <remarks>
    /// should also implement Attribute
    /// </remarks>
    interface IFilterActionAttribute
    {
        bool ShouldConstructController { get; }
        ViewObject HandeleAction(Func<ViewObject> action);
    }
}
