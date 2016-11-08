using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.DataContainers
{
    public class JsonGame
    {
        public int MinimumPlayers { get; set; }
        public int MaximumPlayers { get; set; }
        public string Eann { get; set; }
        public string ApiId { get; set; }
        public string GameTitle { get; set; }
        public List<JsonRelease> Releases { get; set; }
        public int RatingPEGI { get; set; }
        public List<string> Publisher { get; set; }
        public List<string> Genres { get; set; }
        public List<string> PictureUrls { get; set; }
        public string Description { get; set; }

        public JsonGame()
        {
            Releases = new List<JsonRelease>();
            Publisher = new List<string>();
            Genres = new List<string>();
            PictureUrls = new List<string>();
        }
    }
}
