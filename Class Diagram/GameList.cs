using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataModels
{
    public class MyLists
    {
        public string TitleOfList { get; set; }

        public bool IsPrivate { get; set; } = true;

        public List<Game> Games { get; set; }

        public BsonObjectId _id { get; set; } = ObjectId.GenerateNewId();

    }
}