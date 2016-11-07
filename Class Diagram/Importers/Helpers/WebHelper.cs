﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.Helpers
{
    public static class WebHelper
    {
        public static string createUrlWithParameters (string baseUrl, Dictionary<string, string> urlParameters)
        {
            if(urlParameters.Count == 0)
            {
                return baseUrl;
            }

            StringBuilder urlBuilder = new StringBuilder();
            Dictionary<string,string>.Enumerator enumerator = urlParameters.GetEnumerator();
            KeyValuePair<string, string> urlParameterPair;

            urlBuilder.Append(baseUrl);
            enumerator.MoveNext();
            urlParameterPair = enumerator.Current;
            urlBuilder.Append("?" + urlParameterPair.Key + "=" + urlParameterPair.Value);

            while (enumerator.MoveNext())
            {
                urlParameterPair = enumerator.Current;
                urlBuilder.Append("&" + urlParameterPair.Key + "=" + urlParameterPair.Value);
            }

            return urlBuilder.ToString();
        }

        public static HttpClient getDefaultImporterHttpClient()
        {
            HttpClient client = new HttpClient();

            client.Timeout = TimeSpan.FromMilliseconds(5000);
            client.DefaultRequestHeaders.Add("User-Agent", "HR Project5_6 App");

            return client;
        }
    }
}