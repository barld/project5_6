using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class MVCApp
    {
        private ControllerFactory cFactory;

        public string ListenAddress { get; }

        public MVCApp(string listenAddress)
        {
            ListenAddress = listenAddress;
        }

        public void Run()
        {
            cFactory = new ControllerFactory(Assembly.GetEntryAssembly());

            HttpListener listner = new HttpListener();
            listner.Prefixes.Add(ListenAddress);
            Console.WriteLine($"listen on {ListenAddress}");
            listner.Start();


            while (true)
            {

                var context = listner.GetContext();

                var response = context.Response;

                System.IO.Stream body = context.Request.InputStream;
                System.Text.Encoding encoding = context.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                Console.WriteLine(reader.ReadToEnd());

                var rHandler = new RequestHandler(cFactory, context);
                rHandler.HandelToView().Respond(context.Response);

                var controller = cFactory.GetByRawUrl(WebUtility.UrlDecode(context.Request.RawUrl));
            }
        }

        
    }
}
