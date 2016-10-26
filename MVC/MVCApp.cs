using MVC.Routing;
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
        private readonly IEnumerable<IRoute> Routes;
        public string ListenAddress { get; }

        public MVCApp(string listenAddress, IEnumerable<IRoute> routes)
        {
            ListenAddress = listenAddress;
            Routes = routes;
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

                var rHandler = new RequestHandler(Routes, context);
                rHandler.HandelToView().Respond(context.Response);
            }
        }

        
    }
}
