using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool isMale { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string role { get; set; }
        public IEnumerable<UserAddress> addresses;
    }

    public class UserAddress
    {
        public string city { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }
        public string street { get; set; }
        public int snumber { get; set; }
        public bool isDeliveryAddress { get; set; }
    }

    public class Category
    {
        public string platformTitle { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
    }

    public class Game
    {
        public string title { get; set; }
        public List<Category> platforms { get; set; }
        public int ratingPEGI { get; set; }
        public int year { get; set; }
        public string publisher { get; set; }
        public bool isVRCompatible { get; set; }
    }
}
