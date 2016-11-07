using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    //Defining the attribures for the game genre (e.g. Name, Description)
    public class Genre
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ObjectId _id { get; set; }
    }
}