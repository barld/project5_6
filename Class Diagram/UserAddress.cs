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

        public string Streetname { get; set; }

        public string Housenumber { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public ObjectId _id { get; set; }
    }
}
