using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class IDatabaseConnection
    {
        public abstract bool isConnected { get; set; }
        public abstract void checkConnection();
        public abstract void initialize();
        public abstract void reset();
    }
}
