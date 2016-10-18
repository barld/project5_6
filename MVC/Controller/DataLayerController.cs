using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVC.Controller
{
    public class DataLayerController : ControllerObject
    {
        string bodyContent = null;

        protected T GetBodyFromJson<T>()
        {
            if(bodyContent==null)
            {
                System.IO.Stream body = _requestContext.Request.InputStream;
                System.Text.Encoding encoding = _requestContext.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                bodyContent = reader.ReadToEnd();
            }            
            return JsonConvert.DeserializeObject<T>(bodyContent);
        }
    }
}
