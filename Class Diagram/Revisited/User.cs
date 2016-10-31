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

        public Class_Diagram.AccountRole AccountRole
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
