using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Revisited
{
    public class Game
    {
        public string title { get; set; }
        public System.Collections.Generic.ICollection<DataModels.Revisited.Category> platforms { get; set; }
        public int ratingPEGI { get; set; }
        public int year { get; set; }
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

        public ICollection<DataModels.Revisited.Gerne> gernes
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
