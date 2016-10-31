using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DataModels
{
    public class Order
    {
        public int OrderNumber
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime OrderDate
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.User Customer
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public System.Collections.Generic.ICollection<DataModels.OrderLine> OrderLines
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.Address BillingAddress
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.Address DeliveryAddress
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public ObjectId _id
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}