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
        }

        public override void Respond(HttpListenerResponse response)
        {
            var json = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);

            response.AppendHeader("Content-Type", "application/json; charset=utf-8");
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
