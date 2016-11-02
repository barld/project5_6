using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public class AddressGateway : Gateway<Address>
    {
        public AddressGateway(IMongoDatabase connection) : base("Address", connection)
        {
        }

        public async Task<IEnumerable<Address>> GetAllByCity(string city)
        {
            var filter = Builders<Address>.Filter.Eq(a => a.City, city);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAllByCountry(string country)
        {
            var filter = Builders<Address>.Filter.Eq(a => a.Country, country);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}