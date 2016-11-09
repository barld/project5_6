using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.ShoppingCart
{
    public class CartLine
    {
        public int Amount { get; set; }
        public Game Product { get; set; }
        public int SubTotal => Amount * Product.Price;
    }
}
