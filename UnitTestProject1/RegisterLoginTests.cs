using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using DataModels;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class RegisterLoginTests
    {
        private string userEmail = "gregorhoogstad@gmail.com";
        private string userPassword = "geheim321";
        private string mediaType = "Application/JSON";
        private Gender userGender = Gender.Male;
        private AccountRole userRole = AccountRole.User;
        private string sessionCookieName = "id";
        private Uri sessionCookiePath = new Uri("http://localhost/");
        private Uri statusPath = new Uri("http://localhost:8080/api/user/status/");
        private Uri registerPath = new Uri("http://localhost:8080/api/user/register/");
        private Uri loginPath = new Uri("http://localhost:8080/api/user/login/");
        private Uri logoutPath = new Uri("http://localhost:8080/api/user/logout/");

        [TestMethod]
        public void TestRegisterLoginLogout()
        {
            Register();
            LoginLogout();
        }

        public void Register()
        {
            //ARRANGE
            var cookieContainer = new CookieContainer();
            var httpClient = createClient(cookieContainer);

            //ACT
            //Register user
            var registerJsonData = new { email = userEmail, password = userPassword, Gender = userGender };
            var registerPostRequest = new StringContent(JsonConvert.SerializeObject(registerJsonData), Encoding.UTF8, mediaType);
            var registerResponse = httpClient.PostAsync(registerPath, registerPostRequest).Result;
            var registerResultString = registerResponse.Content.ReadAsStringAsync().Result;
            var registerJsonObject = JObject.Parse(registerResultString);
            Assert.IsTrue(registerJsonObject["Success"].ToString().ToLower().Equals("true"));

            //Check session cookie and fix cookie path
            var cookieList = cookieContainer.GetCookies(registerPath);
            Assert.IsTrue(cookieList.Count > 0 && hasCookie(cookieList, sessionCookieName));
            cookieContainer.Add(sessionCookiePath, new Cookie("id", cookieList[0].Value));

            checkLoginStatus(httpClient, true, userRole, userEmail);
        }

        public void LoginLogout()
        {
            //ARRANGE
            var cookieContainer = new CookieContainer();
            var httpClient = createClient(cookieContainer);

            //ACT
            //Check login
            var loginJsonData = new { email = userEmail, password = userPassword, Gender = userGender };
            var loginPostRequest = new StringContent(JsonConvert.SerializeObject(loginJsonData), Encoding.UTF8, mediaType);
            var loginResponse = httpClient.PostAsync(loginPath, loginPostRequest).Result;
            var loginResponseString = loginResponse.Content.ReadAsStringAsync().Result;
            var loginJsonObject = JObject.Parse(loginResponseString);

            Assert.IsTrue(loginJsonObject["Success"].ToString().ToLower().Equals("true"));

            //Check and fix cookie
            fixCookie(cookieContainer, loginPath);

            //Check login status
            checkLoginStatus(httpClient, true, userRole, userEmail);

            //Check logout
            var logoutResponse = httpClient.PutAsync(logoutPath, new StringContent("")).Result;
            var logoutResponseString = logoutResponse.Content.ReadAsStringAsync().Result;

            Assert.IsTrue(logoutResponseString.ToLower().Equals("true"));

            checkLoginStatus(httpClient, false, AccountRole.User, null);
        }

        /// <summary>
        /// Checks if the server correctly remembers the login status.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="expectedResult"></param>
        private void checkLoginStatus(HttpClient httpClient, bool expectedResult, AccountRole expectedRole, string expectedEmail)
        {
            //Check login status
            //ACT
            var loginCheckResponse = httpClient.GetAsync(statusPath).Result;
            var loginCheckResultString = loginCheckResponse.Content.ReadAsStringAsync().Result;
            var loginCheckJsonObject = JObject.Parse(loginCheckResultString);

            //ASSERT
            if (expectedResult)
            {
                Assert.IsTrue(getLowerString(loginCheckJsonObject["IsLogedIn"]).Equals("true"));
                Assert.IsTrue(getLowerString(loginCheckJsonObject["Email"]).Equals(expectedEmail.ToLower()));
                Assert.IsTrue(getLowerString(loginCheckJsonObject["Role"]).Equals(expectedRole.ToString().ToLower()));
            }else
            {
                Assert.IsTrue(getLowerString(loginCheckJsonObject["IsLogedIn"]).Equals("false"));
            }

        }

        private string getLowerString(Object obj)
        {
            if(obj == null)
                return "";

            return obj.ToString().ToLower();
        }

        private bool hasCookie(CookieCollection cl, string cookieName)
        {
            var result = false;
            foreach(Cookie cookie in cl)
            {
                if (cookie.Name.Equals(cookieName)){
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if the server correctly delivered the session cookie and fixes the session cookie for further use
        /// </summary>
        /// <param name="cookieContainer"></param>
        /// <param name="path"></param>
        private void fixCookie(CookieContainer cookieContainer, Uri path)
        {
            //ACT
            var cookieList = cookieContainer.GetCookies(path);

            //ASSERT
            Assert.IsTrue(cookieList.Count > 0 && hasCookie(cookieList, sessionCookieName));

            //FIX
            cookieContainer.Add(sessionCookiePath, new Cookie("id", cookieList[0].Value));
        }

        private HttpClient createClient(CookieContainer container)
        {
            var handler = new HttpClientHandler();
            handler.CookieContainer = container;
            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromMilliseconds(999999);
            return httpClient;
        }
    }
}
