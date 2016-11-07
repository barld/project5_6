using Class_Diagram.Importers.DataContainers;
using Class_Diagram.Importers.Helpers;
using DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;

namespace Class_Diagram.Importers.Impl
{
    public class IGDBGameImporter : GameImporter
    {
        private TimeSpan delay;
        private List<Platform> platforms;
        private HashSet<int> generatedNumbers;

        public IGDBGameImporter()
        {
            generatedNumbers = new HashSet<int>();
            delay = TimeSpan.FromMilliseconds(1050);
        }

        public List<Game> importGames(int desiredMinimumAmount)
        {
            int importedAmount = 0;
            int requestAmount = 0;
            string BASE_URL = "http://www.giantbomb.com/api/games/";

            PlatformImporter pImporter = new IGDBPlatformImporter();
            platforms = pImporter.importPlatforms();
            List<Game> gameList = new List<Game>();

            while (importedAmount < desiredMinimumAmount)
            {
                Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
                {
                    { "api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                    { "format", "json" },
                    { "limit", "50" },
                    { "offset", (50*requestAmount).ToString() },
                    { "field_list", "name,description,image,original_release_date,original_game_rating,id" },
                    { "sort", "original_release_date:desc" },
                    { "filter", "original_release_date:2014-01-01 00:00:00|" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00,platforms:" + String.Join("|", Platforms.getPlatformIds())}
                };

                string responseText = WebHelper.queryApi(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS));
                JObject responseJsonObject = JObject.Parse(responseText);
                List<JsonGame> gicList = new List<JsonGame>();
                JsonGame gic;

                foreach (JObject jro in responseJsonObject["results"].Children())
                {
                    if (jsonIsNull(jro["description"]) || jsonIsNull(jro["original_game_rating"]))
                    {
                        continue;
                    }

                    gic = new JsonGame();
                    gic.ApiId = (string)jro["id"];
                    gic.Description = Regex.Replace((string)jro["description"], "<.*?>", String.Empty);
                    gic.GameTitle = (string)jro["name"];

                    if (!jsonIsNull(jro["image"]))
                    {
                        gic.ImagerHref = (string)jro["image"]["thumb_url"];
                        gic.ThumbnailImageHref = (string)jro["image"]["small_url"];
                    }

                    gic.RatingPEGI = ratingTransformer((string)jro["original_game_rating"][0]["name"]);

                    getDetailedGameData(gic);
                    getReleases(gic, Region.EU);
                    gicList.Add(gic);
                }

                List<Game> tmpList = createGames(gicList);
                gameList.AddRange(tmpList);
                requestAmount++;
                importedAmount += tmpList.Count;
            }

            Debug.WriteLine("Imported " + gameList.Count + " game objects");
            return gameList;
        }

        private List<Game> createGames(List<JsonGame> gicList)
        {
            List<Game> gameList = new List<Game>();
            Random rnd = new Random();
            foreach (JsonGame gic in gicList)
            {
                foreach (JsonRelease rel in gic.Releases)
                {
                    int rndNumber = 0;
                    do
                    {
                        rndNumber = rnd.Next(99999999);
                    } while (!generatedNumbers.Add(rndNumber));
                    Game game = new Game()
                    {
                        Description = gic.Description,
                        EAN = long.Parse("87104" + rndNumber.ToString().PadLeft(8, '0')),
                        GameTitle = rel.ReleaseName,
                        Genres = createGenres(gic),
                        MinPlayers = gic.MinimumPlayers,
                        MaxPlayers = gic.MinimumPlayers,
                        IsVRCompatible = false,
                        Platform = platforms.Find(a => a.PlatformTitle == rel.PlatformName),
                        Image = new List<string>() { gic.ThumbnailImageHref, gic.ImagerHref },
                        Price = rel.Price,
                        Publisher = gic.Publisher,
                        RatingPEGI = gic.RatingPEGI,
                        ReleaseDate = rel.ReleaseDate
                    };
                    gameList.Add(game);
                }
            }
            return gameList;
        }

        private List<Genre> createGenres(JsonGame game)
        {
            List<Genre> genreList = new List<Genre>();
            foreach (string genreTitle in game.Genres)
            {
                genreList.Add(new Genre()
                {
                    Name = genreTitle,
                    Description = ""
                });
            }
            return genreList;
        }

        private bool getDetailedGameData(JsonGame gic)
        {
            string BASE_URL = "http://www.giantbomb.com/api/game/" + gic.ApiId + "/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                { "field_list", "publishers,genres,original_release_date" }
            };
            string responseText = WebHelper.queryApi(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS));
            JObject jro = (JObject)JObject.Parse(responseText)["results"];

            if (!jsonIsNull(jro["publishers"]) || !jsonIsNull(jro["platforms"]) || !jsonIsNull(jro["gernes"]))
            {
                JArray jsonPublishers = (JArray)jro["publishers"];
                gic.Publisher.Add((string)jsonPublishers.First()["name"]);


                JArray jsonGernes = (JArray)jro["genres"];
                foreach (JObject jsonGenre in jsonGernes.Children())
                {
                    gic.Genres.Add((string)jsonGenre["name"]);
                }

                return true;
            }
            return false;
        }

        private void getReleases(JsonGame gameData, Region region)
        {
            string BASE_URL = "http://www.giantbomb.com/api/releases/";
            Dictionary<string, string> URL_PARAMETERS = new Dictionary<string, string>()
            {
                {"api_key", "b40c4c655df8cb22f96bba9bc1e5a506acec250a" },
                { "format", "json" },
                {"field_list", "name,game_rating,minimum_players,maximum_players,platform,release_date" },
                { "filter", "region:"+ (int)region + ",platform"+ String.Join("|", Platforms.getPlatformIds()) + ",game:"+ gameData.ApiId+",platform:146|145|139|94" }
            };
            string responseText = WebHelper.queryApi(WebHelper.createUrlWithParameters(BASE_URL, URL_PARAMETERS));
            JArray jra = (JArray)JObject.Parse(responseText)["results"];

            foreach (JObject jro in jra)
            {
                if (jro["platform"].Type == JTokenType.Null) { continue; }

                gameData.MinimumPlayers = !jsonIsNull(jro["minimum_players"]) ? Int32.Parse((string)jro["minimum_players"]) : 1;
                gameData.MaximumPlayers = !jsonIsNull(jro["maximum_players"]) ? Int32.Parse((string)jro["maximum_players"]) : 4;
                gameData.Releases.Add(new JsonRelease()
                {
                    ReleaseName = !jsonIsNull(jro["name"]) ? (string)jro["name"] : gameData.GameTitle,
                    PlatformName = (string)jro["platform"]["name"],
                    ReleaseDate = DateTime.Parse(!jsonIsNull(jro["release_date"]) ? (string)jro["release_date"] : ""),
                    Price = 5000
                });
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

        private bool jsonIsNull(JToken jsonObject)
        {
            return jsonObject == null || jsonObject.Type == JTokenType.Null;
        }

        private void wait()
        {
            Thread.Sleep(1200);
        }

        private enum Region
        {
            EU = 1
        }
    }
}
