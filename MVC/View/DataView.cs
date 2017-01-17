using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVC.View
{
    public abstract class DataView : ViewObject
    {
        protected readonly object data;
        protected abstract string SerializeObject();

        public DataView(object data)
        {
            this.data = data;
            this.Content = SerializeObject();
        }
    }
}
