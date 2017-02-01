using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Statistics
{
    public class GenreStatisticModel
    {
        public DateTime Date { get; set; }
        public Dictionary<String, int> GenreAmounts { get; set; }
        public int DateTotal { get; set; }
        public String KeyString { get; set; }
    }
}
