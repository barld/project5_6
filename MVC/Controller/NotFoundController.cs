using System;

namespace MVC.Controller
{
    internal class NotFoundController : ControllerObject
    {
        public override object Get()
        {
            return "<h1>404 Not Found</h1>";
        }
    }
}