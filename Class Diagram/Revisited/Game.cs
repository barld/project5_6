using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.Revisited;

namespace DataModels.Revisited
{
    public class Game
    {
        public string gameTitle { get; set; }
        public List<Platform> platform { get; set; }
        public int ratingPEGI { get; set; }
        public string publisher { get; set; }

        public List<Genre> genres
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string image
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int minPlayers
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int maxPlayers
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string description
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
