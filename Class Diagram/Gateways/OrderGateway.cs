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
        enum TimeGroup
        {
            Day,
            Week,
            Month
        }

        public async Task<Dictionary<DateTime, int>> GetOrderAmountData(TimeGroup timeSpan, DateTime beginDate, DateTime endDate)
        {
            //timeSpan.d

            var filter = Builders<Order>.Filter.Where(v => v.OrderDate > beginDate && v.OrderDate < endDate);
            var result = Collection.Find(filter).ToList();

            var groupedResult = new Dictionary<DateTime, int>();
            switch (timeSpan)
            {
                case TimeGroup.Day:
                    groupedResult = result.GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, g.OrderDate.Day)).ToDictionary(x => x.Key, x => x.Count());
                    break;
                case TimeGroup.Week:
                    break;
                case TimeGroup.Month:
                    break;
                default:
                    break;
            }

            return groupedResult;
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