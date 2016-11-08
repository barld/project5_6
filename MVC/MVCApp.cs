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
        private Dictionary<string, Session> sessions = new Dictionary<string, Session>();

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
                try
                {
                    var context = listner.GetContext();
                    Console.WriteLine(context.Request.Url);

                    var response = context.Response;
                    string cValue = string.Empty;

                    if (context.Request.Cookies["id"] == null || (context.Request.Cookies["id"] != null && !sessions.ContainsKey(context.Request.Cookies["id"].Value)))
                    {
                        var cookie = new Cookie("id", WebUtility.UrlEncode(Crypto.Keys.GetRandomKey(20)));

                        cookie.Secure = true;
                        cookie.HttpOnly = true;

                        context.Response.SetCookie(cookie);

                        if (!sessions.ContainsKey(cookie.Value))
                            sessions.Remove(cookie.Value);
                        sessions.Add(cookie.Value, new Session { Data = new Dictionary<string, object>() });
                        cValue = cookie.Value;
                    }
                    else
                    {
                        cValue = context.Request.Cookies["id"].Value;
                    }

                    Console.WriteLine(context.Request.HttpMethod);

                    var session = sessions[cValue];

                    var rHandler = new RequestHandler(Routes, context);
                    rHandler.HandelToView(session).Respond(context.Response);
                }
                catch(Exception e) { Console.WriteLine(e); }//catch all exceptions. otherwise the server will crash                
            }
        }

        
    }
}
