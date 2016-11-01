using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    public class MyLists
    {
        public string TitleOfList { get; set; }

        public List<Game> Games { get; set; }

        public ObjectId _id { get; set; }
    }
}