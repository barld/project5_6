using MVC.View;
using System;

namespace MVC.Controller
{
    internal class NotFoundController : ControllerObject
    {
        public object Get()
        {
            return new NotFoundView("from not found controller");
        }
    }
}