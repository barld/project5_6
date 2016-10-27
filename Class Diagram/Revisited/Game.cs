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
        public string title { get; set; }
        public Platform platform { get; set; }
        public int ratingPEGI { get; set; }
        public DateTime releaseData { get; set; }
        public string publisher { get; set; }
        public bool isVRCompatible { get; set; }

        public decimal price
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public ICollection<Gerne> gernes
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
