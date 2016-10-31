using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace DataModels
{
    public class Platform
    {
        public string PlatformTitle { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }

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
    }
}
