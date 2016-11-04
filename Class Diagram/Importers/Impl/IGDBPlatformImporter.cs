using Class_Diagram.Importers.DataContainers;
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
using System.Text.RegularExpressions;
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
            { "field_list", "name,description,company,release_date" },
            { "filter","release_date:2012-01-01 01:01:01|2017-01-01 01:01:01" },
            { "sort", "release_date:desc" }
        };

        public List<Platform> importPlatforms()
        {
            bool finished = false;
            List<Platform> platforms = new List<Platform>();
            Platform platform;

            JObject jsonResponse;
            JArray jra;

            string platformName, platformBrand, platformDescription;

            HttpClient httpClient = WebHelper.getDefaultImporterHttpClient();

            while (finished == false)
            {
                HttpResponseMessage response = httpClient.GetAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
                string resultString = response.Content.ReadAsStringAsync().Result;
                jsonResponse = JObject.Parse(resultString);
                jra = (JArray) jsonResponse["results"];

                foreach(JObject jro in jra)
                {

                    if (jro["name"].Type == JTokenType.String) {
                        platformName = (string)jro["name"];

                        if (jro["company"].Type == JTokenType.Object)
                        {
                            platformBrand = (string)jro["company"]["name"];
                        }
                        else
                        {
                            platformBrand = "NA";
                        }

                        platformDescription = jro["description"].Type != JTokenType.Null ? Regex.Replace((string)jro["description"], "<.*?>", String.Empty) : "";

                        platform = new Platform()
                        {
                            PlatformTitle = platformName,
                            Brand = platformBrand,
                            Description = platformDescription
                        };

                        platforms.Add(platform);
                    }
                }
                finished = true;
            }
            return platforms;
        }
    }
}
