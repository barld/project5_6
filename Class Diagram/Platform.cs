﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace DataModels
{
    //Defining the details of a platform (e.g. id, title, brand and description
    public class Platform
    {
        public ObjectId _id { get; set; }
        public string PlatformTitle { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
    }
}
