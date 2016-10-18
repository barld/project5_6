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
        public MVCApp(string listenAddress) : this(listenAddress, Assembly.GetEntryAssembly()) { }

        public MVCApp(string listenAddress, Assembly assembly)
        {
            ListenAddress = listenAddress;
            cFactory = new ControllerFactory(assembly);
        }

        public MVCApp(string listenAddress, IEnumerable<Type> controllers)
        {
            ListenAddress = listenAddress;
            cFactory = new ControllerFactory(controllers);
        }

        public void Run()
        {

            HttpListener listner = new HttpListener();
            listner.Prefixes.Add(ListenAddress);
            Console.WriteLine($"listen on {ListenAddress}");
            listner.Start();


            while (true)
            {

                var context = listner.GetContext();

                var response = context.Response;

                Console.WriteLine(context.Request.HttpMethod);

                var rHandler = new RequestHandler(cFactory, context);
                rHandler.HandelToView().Respond(context.Response);

                var controller = cFactory.GetByRawUrl(WebUtility.UrlDecode(context.Request.RawUrl));
            }
        }

        
    }
}
