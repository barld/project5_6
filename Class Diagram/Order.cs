using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    //Defining the order specifications (Ordernumber, date etc.)
    public class Order
    {
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public User Customer { get; set; }

        public IEnumerable<OrderLine> OrderLines { get; set; }

        public Address BillingAddress { get; set; }
        public Address DeliveryAddress { get; set; }

        public ObjectId _id { get; set; }
    }
}