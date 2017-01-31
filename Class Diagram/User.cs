using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataModels
{
    //Defining the details of a user (Email, gender etc.)
    public class User
    {
        public string Password { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataModels.Gender Gender { get; set; }

        public List<MyLists> MyLists { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountRole AccountRole { get; set; }

        public string Salt { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
