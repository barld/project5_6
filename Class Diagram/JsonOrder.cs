using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Class_Diagram.ShoppingCart;

namespace DataModels
{
    //Defining the attributes for the orders (Email, city etc.)
    public class JsonOrder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Cart shoppingcart { get; set; }
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