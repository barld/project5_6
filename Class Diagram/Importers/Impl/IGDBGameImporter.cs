using Class_Diagram.Importers.Helpers;
using DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.Impl
{
    public class IGDBGameImporter : GameImporter
    {
        private TimeSpan delay = TimeSpan.FromMilliseconds(1050);

        public void importGames(int desiredAmount)
        {
            getBasicGameData(desiredAmount);
        }

        private List<Game> getBasicGameData(int desiredAmount)
        {
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
                {"filter", "original_release_date:2014-01-01 00:00:00|" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"}
            };
            HttpClient client = WebHelper.getDefaultImporterHttpClient();

            string responseText = client.GetStringAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
            JObject responseJsonObject = JObject.Parse(responseText);
            List<JsonGameInfoContainer> gicList = new List<JsonGameInfoContainer>();
            JsonGameInfoContainer gic;

            foreach(JObject gameJsonObject in responseJsonObject["results"].Children())
            {
                if(gameJsonObject["description"].Type == JTokenType.Null || gameJsonObject["original_game_rating"].Type == JTokenType.Null)
                {
                    continue;
                }

                gic = new JsonGameInfoContainer();
                gic.ApiId = (string) gameJsonObject["id"];
                gic.Description = (string)gameJsonObject["description"];
                gic.GameTitle = (string)gameJsonObject["name"];

                if (gameJsonObject["image"].Type != JTokenType.Null) {
                    gic.ImagerHref = (string)gameJsonObject["image"]["thumb_url"];
                    gic.ThumbnailImageHref = (string)gameJsonObject["image"]["small_url"];
                }

                gic.RatingPEGI = transformRating((string)gameJsonObject["original_game_rating"]["name"]);
            }

            return null;
        }

        private void getDetailedGameData(JsonGameInfoContainer gameInfoContainer)
        {
            string BASE_URL = "http://www.giantbomb.com/api/games/" + gameInfoContainer.ApiId + "/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                { "field_list", "publishers" }
            };
            HttpClient client = WebHelper.getDefaultImporterHttpClient();

            string responseText = client.GetStringAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
        }

        private void getReleaseData(JsonGameInfoContainer gameInfoContainer)
        {
            string BASE_URL = "http://www.giantbomb.com/api/games/" + gameInfoContainer.ApiId + "/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                { "field_list", "publishers" }
            };
            HttpClient client = WebHelper.getDefaultImporterHttpClient();

            string responseText = client.GetStringAsync(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS)).Result;
        }

        private string transformRating(string originalRating)
        {
            return "10+";
        }

        private class JsonGameInfoContainer
        {
            public string ApiId { get; set; }
            public string GameTitle { get; set; }
            public int PlatformId { get; set; }
            public string RatingPEGI { get; set; }
            public string Publisher { get; set; }
            public List<int> GenreIds { get; set; }
            public string ThumbnailImageHref { get; set; }
            public string ImagerHref { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public DateTime ReleaseDate { get; set; }
        }
    }
}
