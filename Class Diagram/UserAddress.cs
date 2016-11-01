using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataModels
{
    public class Address
    {
        public string City { get; set; }

        public string Streetname
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Housenumber
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Country
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string PostalCode
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
