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
using System.Diagnostics;
using System.Threading;

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

        public List<SaleStatisticDataModel> GetOrderAmountDataTask(TimeScale timeScale, DateTime beginDate, DateTime endDate)
        {
            //timeSpan.d
            CultureInfo ci = CultureInfo.CreateSpecificCulture("nl-NL");
            Thread.CurrentThread.CurrentCulture = ci;

            var filter = Builders<Order>.Filter.Where(v => v.OrderDate >= beginDate && v.OrderDate < endDate.AddDays(1));
            var result = Collection.Find(filter).ToList();
            var calander = ci.DateTimeFormat.FirstDayOfWeek;

            var groupedResult = new List<SaleStatisticDataModel>();
            switch (timeScale)
            {
                case TimeScale.Day:
                    groupedResult = result
                        .GroupBy(g => g.OrderDate.Date)
                        .Select(grp => new SaleStatisticDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = grp.Key.Date.ToShortDateString() })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                case TimeScale.Week:
                    groupedResult = result
                        .GroupBy(g => firstDayOfWeek(g.OrderDate))
                        .Select(grp => new SaleStatisticDataModel() { Date=grp.Key.Date,Amount=grp.Count(),KeyString= "W" + ci.Calendar.GetWeekOfYear(grp.Key, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek) + " - " + grp.Key.Year })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                case TimeScale.Month:
                    groupedResult = result
                        .GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, 1))
                        .Select(grp=> new SaleStatisticDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = ci.DateTimeFormat.GetMonthName(grp.Key.Month) + " - " + grp.Key.Year })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                default:
                    break;
            }

            return groupedResult;
        }

        public int GetLatestOrderNumber()
        {
            try
            {
                var order = Collection.Find(new BsonDocument()).SortByDescending(x => x.OrderNumber).FirstOrDefault();
                return order.OrderNumber;
            }
            catch
            {
                return 0;
            }
        }

        private DateTime firstDayOfWeek(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset).Date;
            Debug.WriteLine(fdowDate);
            return fdowDate;
        }
    }
}