using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataModels
{
    //Defining the attributes of an address (city, street etc.)
    public class Address
    {
        public string City { get; set; }

        public string Streetname { get; set; }

        public string Housenumber { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
}
