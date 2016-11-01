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

        public List<Genre> Genres
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Image
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int MinPlayers
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int MaxPlayers
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Description
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public ObjectId _id
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int EAN
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Price
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool IsVRCompatible
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
