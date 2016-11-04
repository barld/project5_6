using Class_Diagram.Importers.Helpers;
using DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.Impl
{
    public class IGDBGameImporter : GameImporter
    {
        private TimeSpan delay = TimeSpan.FromMilliseconds(1050);

        public void importGames(List<PlatformId> desiredPlatforms,int desiredAmount)
        {
            var platformIds = new List<int>();
            foreach(PlatformId id in desiredPlatforms)
            {
                platformIds.Add((int)id);
            }

            int importedAmount = 0;
            int foundAmount;
            string BASE_URL = "http://www.giantbomb.com/api/games/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                { "limit", desiredAmount < 100 ? desiredAmount.ToString() : "100" },
                { "offset", "0" },
                { "field_list", "name,description,image,original_release_date,original_game_rating,id" },
                {"sort", "original_release_date:desc" },
                {"filter", "original_release_date:2014-01-01 00:00:00|" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00,platforms:" + String.Join("|", platformIds)}
            };
            HttpClient client = WebHelper.getDefaultImporterHttpClient();

            string responseText = client.GetStringAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
            JObject responseJsonObject = JObject.Parse(responseText);
            List<JsonGameInfoContainer> gicList = new List<JsonGameInfoContainer>();
            JsonGameInfoContainer gic;

            foreach (JObject gameJsonObject in responseJsonObject["results"].Children())
            {
                if (gameJsonObject["description"].Type == JTokenType.Null || gameJsonObject["original_game_rating"].Type == JTokenType.Null)
                {
                    continue;
                }

                gic = new JsonGameInfoContainer();
                gic.ApiId = (string)gameJsonObject["id"];
                gic.Description = Regex.Replace((string)gameJsonObject["description"], "<.*?>", String.Empty) ;
                gic.GameTitle = (string)gameJsonObject["name"];

                if (gameJsonObject["image"].Type != JTokenType.Null)
                {
                    gic.ImagerHref = (string)gameJsonObject["image"]["thumb_url"];
                    gic.ThumbnailImageHref = (string)gameJsonObject["image"]["small_url"];
                }

                gic.RatingPEGI = ratingTransformer((string)gameJsonObject["original_game_rating"][0]["name"]);

                getDetailedGameData(gic);
                //getReleases(platformIds, gic, Region.EU);
                gicList.Add(gic);
            }

            Debug.WriteLine("Imported " + gicList.Count + " games");
        }

        private bool getDetailedGameData(JsonGameInfoContainer gic)
        {
            string BASE_URL = "http://www.giantbomb.com/api/game/" + gic.ApiId + "/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                { "field_list", "publishers,genres,original_release_date" }
            };
            HttpClient client = WebHelper.getDefaultImporterHttpClient();

            wait();
            string responseText = client.GetStringAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
            JObject jsonResponseObject = (JObject) JObject.Parse(responseText)["results"];

            if (jsonResponseObject["publishers"].Type != JTokenType.Null || jsonResponseObject["platforms"].Type != JTokenType.Null || jsonResponseObject["genres"].Type != JTokenType.Null)
            {
                JArray jsonPublishers = (JArray)jsonResponseObject["publishers"];
                gic.Publisher.Add((string)jsonPublishers.First()["name"]);


                JArray jsonGernes = (JArray)jsonResponseObject["genres"];
                foreach (JObject jsonGenre in jsonGernes.Children())
                {
                    gic.Genres.Add((string)jsonGenre["name"]);
                }

                return true;
            }
            return false;
        }

        private void getReleases(List<int> platformIds, JsonGameInfoContainer gameData, Region region)
        {
            string BASE_URL = "http://www.giantbomb.com/api/releases/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                {"field_list", "name,game_rating,minimum_players,maximum_players,platform,release_date" },
                { "filter", "region:"+ region + ",platform"+ String.Join("|", platformIds) + ",game:"+ gameData.ApiId }
            };
            HttpClient client = WebHelper.getDefaultImporterHttpClient();
            wait();
            string responseText = client.GetStringAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
            JArray jsonResponseObject = (JArray) JObject.Parse(responseText)["results"];

            foreach(JObject jsonRelease in jsonResponseObject)
            {
                gameData.MinimumPlayers = jsonRelease["minimum_players"].Type != JTokenType.Null ? Int32.Parse((string)jsonRelease["minimum_players"]) : 1;
                gameData.Platform.Add(new Tuple<string, DateTime, int>(jsonRelease["platform"]["name"]))
            }
        }

        private int ratingTransformer(string originalRating)
        {
            Dictionary<string, int> ratingDictionary = new Dictionary<string, int>()
            {
                { "ERSB: E", 10 },
                { "ESRB: M", 18 }
            };
            int ratingValue = ratingDictionary.TryGetValue(originalRating, out ratingValue) ? ratingValue : 8;

            return ratingValue;
        }

        private string platformTransformer(string originalPlatform)
        {
            Dictionary<string, string> platformDictionary = new Dictionary<string, string>()
            {
                { "Wii U", "Wii U"},
                {"Xbox One", "Xbox One" },
                {"PlayStation 4", "PlayStation 4" },
                {"PC","PC" }
            };
            string platformValue = platformDictionary.TryGetValue(originalPlatform, out platformValue) ? platformValue : "";

            return platformValue;
        }

        private string gerneTransformer(string originalGerne)
        {
            Dictionary<string, string> gerneDictionary = new Dictionary<string, string>()
            {
                {"","" }
            };

            string gerneValue = gerneDictionary.TryGetValue(originalGerne, out gerneValue) ? gerneValue : "";

            return gerneValue;
        }

        private void wait()
        {
            Thread.Sleep(1050);
        }

        private enum Region
        {
            EU = 1
        }

        private class JsonGameInfoContainer
        {
            public JsonGameInfoContainer()
            {
                Platform = new List<Tuple<string, DateTime, int>>();
                Publisher = new List<string>();
                Genres = new List<string>();
            }
            public int MinimumPlayers { get; set; }
            public int MaximumPlayers { get; set; }
            public string Eann { get; set; }
            public string ApiId { get; set; }
            public string GameTitle { get; set; }
            public List<Tuple<string, DateTime, int>> Platform { get; set; }
            public int RatingPEGI { get; set; }
            public List<string> Publisher { get; set; }
            public List<string> Genres { get; set; }
            public string ThumbnailImageHref { get; set; }
            public string ImagerHref { get; set; }
            public string Description { get; set; }
        }
    }
}
