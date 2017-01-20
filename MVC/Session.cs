using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Session
    {
        public Dictionary<string, object> Data { get; internal set; } = new Dictionary<string, object>();
    }
}
