using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.ShoppingCart
{
    public class Cart
    {
        public int TotalPrice => CartLines.Sum(cl => cl.Product.Price * cl.Amount);
        public IEnumerable<CartLine> CartLines { get; set; } = new List<CartLine>();
    }
}
