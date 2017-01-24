using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataModels.Gateways
{
    public class OrderGateway : Gateway<Order>
    {
        public OrderGateway(IMongoDatabase connection) : base("Order", connection)
        {
        }

        public async Task<Order> GetByOrderNumber(int orderNumber)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.OrderNumber, orderNumber);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByCustomer_id(string id)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Customer_Id, id);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByDeliveryAddress(Address address)
        {
            var filter = Builders<Order>.Filter.Eq(g => g.DeliveryAddress, address);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByBillingAddress(Address address)
        {
            var filter = Builders<Order>.Filter.Eq(g => g.BillingAddress, address);
            return await Collection.Find(filter).ToListAsync();
        }

        public int GetLatestOrderNumber()
        {
            try{
                var order = Collection.Find(new BsonDocument()).SortByDescending(x => x.OrderNumber).FirstOrDefault();
                return order.OrderNumber;
            }
            catch
            {
                return 0;
            }
        }
    }
}