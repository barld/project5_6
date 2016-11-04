using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.DataContainers
{
    public class JsonRelease
    {
        public string ReleaseName { get; set; }
        public string PlatformName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
    }
}
