using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Statistics
{
    public class GameWishListStatisticsJsonDataModel
    {
        public string GameTitle { get; set; }
        public int Count { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as GameWishListStatisticsJsonDataModel;
            if(item == null)
            {
                return false;
            }

            return item.GameTitle.Equals(this.GameTitle);
        }

        public override int GetHashCode()
        {
            return this.GameTitle.GetHashCode();
        }
    }
}
