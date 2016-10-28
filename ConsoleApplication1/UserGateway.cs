using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class UserGateway : Gateway<User>
    {
        public UserGateway(IMongoDatabase connection) : base("user", connection)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.email, email);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}