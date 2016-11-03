using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    public class OrderLine
    {
        public Game Game { get; set; }

        public int Amount { get; set; }

        public ObjectId _id { get; set; }
    }
}