using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Helper
{
    public static class IntegrationHelper
    {
        /// <summary>
        /// Checks if the server correctly delivered the session cookie and fixes the session cookie for further use
        /// </summary>
        /// <param name="cookieContainer"></param>
        /// <param name="recievedCookiePath"></param>
        public static void fixCookie(CookieContainer cookieContainer, Uri recievedCookiePath, Uri targetPath)
        {
            //ACT
            var cookieList = cookieContainer.GetCookies(recievedCookiePath);

            //ASSERT
            Assert.IsTrue(cookieList.Count > 0 && hasCookie(cookieList, "id"));

            //FIX
            cookieContainer.Add(targetPath, new Cookie("id", cookieList[0].Value));
        }

        public static HttpClient createClient(CookieContainer container)
        {
            var handler = new HttpClientHandler();
            handler.CookieContainer = container;
            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromMilliseconds(999999);
            return httpClient;
        }

        public static bool hasCookie(CookieCollection cl, string cookieName)
        {
            var result = false;
            foreach (Cookie cookie in cl)
            {
                if (cookie.Name.Equals(cookieName))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static string getLowerString(Object obj)
        {
            if (obj == null)
                return "";

            return obj.ToString().ToLower();
        }
    }
}
