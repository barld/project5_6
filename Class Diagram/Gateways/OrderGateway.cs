using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Globalization;
using Class_Diagram;

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

        public Dictionary<DateTime, int> GetOrderAmountDataTask(TimeGroup timeSpan, DateTime beginDate, DateTime endDate)
        {
            //timeSpan.d
            CultureInfo ci = CultureInfo.CreateSpecificCulture("nl-NL");

            var filter = Builders<Order>.Filter.Where(v => v.OrderDate > beginDate && v.OrderDate < endDate);
            var result = Collection.Find(filter).ToList();
            var calander = ci.DateTimeFormat.FirstDayOfWeek;

            var groupedResult = new Dictionary<DateTime, int>();
            switch (timeSpan)
            {
                case TimeGroup.Day:
                    groupedResult = result.GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, g.OrderDate.Day)).ToDictionary(x => x.Key, x => x.Count());
                    break;
                case TimeGroup.Week:
                    groupedResult = result.GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, firstDayOfWeek(g.OrderDate))).ToDictionary(x => x.Key, x => x.Count());
                    break;
                case TimeGroup.Month:
                    groupedResult = result.GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, 1)).ToDictionary(x => x.Key, x => x.Count());
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

        private int firstDayOfWeek(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate.Day;
        }
    }
}