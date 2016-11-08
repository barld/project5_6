using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataModels
{
    //Defining the details of a platform (e.g. id, title, brand and description
    public class Platform
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string PlatformTitle { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}
