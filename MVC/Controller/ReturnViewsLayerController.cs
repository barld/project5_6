using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Controller
{
    public class ReturnViewsLayerController : ControllerObject
    {
        protected JsonDataView Json(object data) => new JsonDataView(data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>TODO: choose to return in right form</remarks>
        /// <returns></returns>
        protected DataView AsData(object data) => new JsonDataView(data);
        protected RawObjectView AsRawData(object o) => new RawObjectView(o);
        protected NotFoundView NotFound() => new NotFoundView();
        protected NotFoundView NotFound(string message) => new NotFoundView(message);
    }
}
