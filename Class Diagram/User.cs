using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataModels
{
    public class User
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public DataModels.Gender Gender { get; set; }

        public List<MyLists> MyLists { get; set; }
        public DataModels.AccountRole AccountRole { get; set; }

        public string Salt { get; set; }

        public ObjectId _id { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
