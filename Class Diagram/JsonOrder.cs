using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    //Defining the attributes for the orders (Email, city etc.)
    public class JsonOrder
    {
        public List<long> EAN { get; set; }
        public List<int> Amounts { get; set; }
        public string Email { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryHousenumber { get; set; }
        public string DeliveryPostalCode { get; set; }
        public string DeliveryStreetname { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingHousenumber { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingStreetname { get; set; }
    }
}