using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Statistics
{
    public class SaleStatisticsJsonDataModel
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string KeyString { get; set; }
    }
}
