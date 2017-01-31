using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using DataModels;
using System.Net;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private readonly HttpClient _client;
        public UnitTest1()
        {
            
        }


        [TestMethod]

        public void RegisterAndLogin()
        {
            // arrange
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromMilliseconds(999999);

            var userEmail = "gregorhoogstad@gmail.com";
            var userPassword = "geheim321";
            var userGender = Gender.Male;
            var userRole = AccountRole.User;

            //Register user
            var registerJsonData = new { email = userEmail, password = userPassword, Gender = userGender };
            var registerPostRequest = new StringContent(JsonConvert.SerializeObject(registerJsonData), Encoding.UTF8, "Application/JSON");
            var registerResponse = httpClient.PostAsync("http://localhost:8080/api/user/register/", registerPostRequest).Result;
            var registerResultString = registerResponse.Content.ReadAsStringAsync().Result;
            var registerJsonObject = JObject.Parse(registerResultString);
            //Assert.IsTrue(registerJsonObject["Success"].ToString().ToLower().Equals("true"));

            //Login user
            var loginJsonData = new { email = userEmail, password = userPassword };
            var loginPostRequest = new StringContent(JsonConvert.SerializeObject(loginJsonData), Encoding.UTF8, "Application/JSON");
            var loginResponse = httpClient.PostAsync("http://localhost:8080/api/user/login/", loginPostRequest).Result;
            var loginResultString = loginResponse.Content.ReadAsStringAsync().Result;
            var loginJsonObject = JObject.Parse(loginResultString);
            Assert.IsTrue(loginJsonObject["Success"].ToString().ToLower().Equals("true"));

            //Check login status
            var loginCheckResponse = httpClient.GetAsync("http://localhost:8080/api/user/login/").Result;
            var loginCheckResultString = loginCheckResponse.Content.ReadAsStringAsync().Result;
            var loginCheckJsonObject = JObject.Parse(loginCheckResultString);
            Assert.IsTrue(getLowerString(loginCheckJsonObject["Email"]).Equals(userEmail));
            Assert.IsTrue(getLowerString(loginCheckJsonObject["Role"]).Equals(userRole.ToString()));
        }

        private string getLowerString(Object obj)
        {
            if(obj == null)
                return "";

            return obj.ToString().ToLower();
        }
    }
}
