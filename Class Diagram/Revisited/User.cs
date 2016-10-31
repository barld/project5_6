using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Revisited
{
    public class User
    {
        public string password { get; set; }
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

        public DataModels.Revisited.Gender Gender
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DataModels.Revisited.MyLists gameList
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<MyLists> myLists
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string role
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
