using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using MongoDB.Bson;

namespace DataModels
{
    public class Game
    {
        public string GameTitle { get; set; }
        public Platform Platform { get; set; }
        public int RatingPEGI { get; set; }
        public string Publisher { get; set; }

        public List<Genre> Genres { get; set; }

        public string Image { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public string Description { get; set; }

        public ObjectId _id { get; set; }

        public long EAN { get; set; }

        public int Price { get; set; }

        public bool IsVRCompatible { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
