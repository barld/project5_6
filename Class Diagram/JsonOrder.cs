using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    public class JsonOrder
    {
        public List<long> EAN { get; set; }

        public List<int> Amounts { get; set; }
    }
}