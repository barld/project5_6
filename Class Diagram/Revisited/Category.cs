using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Revisited
{
    public class Platform
    {
        public string platformTitle { get; set; }
        public string brand { get; set; }
        public string description { get; set; }

        public int price
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

        public DateTime releaseDate
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool isVRCompatible
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
