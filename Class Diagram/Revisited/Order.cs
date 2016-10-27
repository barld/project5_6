using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModels.Revisited
{
    public class Order
    {
        public int orderNumber
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime orderDate
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.Revisited.User customer
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public System.Collections.Generic.ICollection<DataModels.Revisited.OrderLine> OrderLines
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.Revisited.Address billingAddress
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.Revisited.Address deliveryAddress
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