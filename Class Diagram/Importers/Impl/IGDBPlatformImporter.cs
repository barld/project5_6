using Class_Diagram.Importers.Helpers;
using DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.Impl
{
    public class IGDBPlatformImporter : PlatformImporter
    {
        private string BASE_URL = "http://www.giantbomb.com/api/platforms/";
        private Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
        {
            {"api_key", "" },
            { "format", "json" },
            { "field_list", "name,company,release_date" },
            { "filter","release_date:2012-01-01 01:01:01|2017-01-01 01:01:01" }
        };

        public void importPlatforms()
        {
            bool finished = false;
            List<Platform> platforms;

            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMilliseconds(5000);
            JObject jsonResponse;
            JArray jsonPlatformArray;
            JObject jsonPlatformObject;

            while (finished == false)
            {
                HttpResponseMessage response = httpClient.GetAsync(UrlCreator.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
                //string jsonString = response.Content.ReadAsStringAsync().Result;
                //Debug.WriteLine(jsonString);
                
            }
        }
    }
}
