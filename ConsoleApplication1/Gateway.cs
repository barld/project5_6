using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Gateway<Model>
    {
        public virtual IEnumerable<Model> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}