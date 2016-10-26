using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class IDatabaseConnection
    {
        public abstract bool isConnected { get; set; }
        public abstract bool isGeneratedWithSampleData { get; set; }
        public abstract bool checkConnection();
        public abstract void initialize();
        public abstract void reset();
        public abstract void setupSampleData();
    }
}
