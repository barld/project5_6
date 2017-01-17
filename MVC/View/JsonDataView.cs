using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVC.View
{
    public class JsonDataView : DataView
    {
        public JsonDataView(object data) : base(data)
        {
            this.ContentType = "application/json; charset=utf-8";
        }

        protected override string SerializeObject() => 
            JsonConvert.SerializeObject(data);
    }
}
