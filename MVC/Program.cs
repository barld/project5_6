using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    class Program
    {
        static ControllerFactory cFactory;

        static void Main(string[] args)
        {
            
            cFactory = new ControllerFactory(Assembly.GetEntryAssembly());

            HttpListener listner = new HttpListener();
            listner.Prefixes.Add("http://127.0.0.1:8080/");
            Console.WriteLine("listen on :8080");
            listner.Start();


            while(true)
            {
                var context = listner.GetContext();

                var response = context.Response;

                System.IO.Stream body = context.Request.InputStream;
                System.Text.Encoding encoding = context.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                Console.WriteLine(reader.ReadToEnd());

                var controller = cFactory.GetByRawUrl(context.Request.RawUrl);

                var buffer = System.Text.Encoding.UTF8.GetBytes(controller.Get().ToString());

                response.AppendHeader("Content-Type", "text/html; charset=utf-8");
                response.ContentLength64 = buffer.Length;

                var output = response.OutputStream;

                output.Write(buffer, 0, buffer.Length);

                //Console.WriteLine(output);

                output.Close();
            }

            
        }
    }
}
