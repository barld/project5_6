using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    public class Order
    {
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DataModels.User Customer { get; set; }

        public System.Collections.Generic.ICollection<DataModels.OrderLine> OrderLines { get; set; }

        public DataModels.Address BillingAddress { get; set; }
        public DataModels.Address DeliveryAddress { get; set; }

        public ObjectId _id { get; set; }
    }
}