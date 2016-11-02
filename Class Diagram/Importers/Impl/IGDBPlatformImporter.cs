using Class_Diagram.Importers.Dataclasses;
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
            {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
            { "format", "json" },
            { "field_list", "name,company,release_date" },
            { "filter","release_date:2012-01-01 01:01:01|2017-01-01 01:01:01" }
        };

        public void importPlatforms()
        {
            bool finished = false;
            List<Platform> platforms = new List<Platform>();
            Platform platform;

            JObject jsonResponse;
            JArray jsonPlatformArray;

            string platformName, platformBrand;

            HttpClient httpClient = WebHelper.getDefaultImporterHttpClient();

            while (finished == false)
            {
                HttpResponseMessage response = httpClient.GetAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
                string resultString = response.Content.ReadAsStringAsync().Result;
                jsonResponse = JObject.Parse(resultString);
                jsonPlatformArray = (JArray) jsonResponse["results"];

                foreach(JObject jsonPlatformObject in jsonPlatformArray)
                {

                    if (jsonPlatformObject["name"].Type == JTokenType.String) {
                        platformName = (string)jsonPlatformObject["name"];

                        if (jsonPlatformObject["company"].Type == JTokenType.Object)
                        {
                            platformBrand = (string)jsonPlatformObject["company"]["name"];
                        }
                        else
                        {
                            platformBrand = "NA";
                        }

                        platform = new Platform()
                        {
                            PlatformTitle = platformName,
                            Brand = platformBrand,
                            Description = ""
                        };

                        platforms.Add(platform);
                    }
                }
                //string jsonString = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("finished");
            }
        }
    }
}
