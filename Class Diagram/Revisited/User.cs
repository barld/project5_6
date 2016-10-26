using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Revisited
{
    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public bool isMale { get; set; }
        public string email { get; set; }

        public System.Collections.Generic.List<DataModels.Revisited.Address> userAddresses
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
