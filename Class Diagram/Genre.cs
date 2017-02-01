using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataModels
{
    //Defining the attribures for the game genre (e.g. Name, Description)
    public class Genre
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as Genre;
            if(o == null)
            {
                return false;
            }
            return o.Name.Equals(this.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}