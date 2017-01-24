using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Order>> GetAllByEmail(string email)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Customer.Email, email);
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

            var result = GetOrdersBetweenDates(beginDate, endDate);
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
                        .Select(grp => new SaleStatisticDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = "W" + ci.Calendar.GetWeekOfYear(grp.Key, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek) + " - " + grp.Key.Year })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                case TimeScale.Month:
                    groupedResult = result
                        .GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, 1))
                        .Select(grp => new SaleStatisticDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = ci.DateTimeFormat.GetMonthName(grp.Key.Month) + " - " + grp.Key.Year })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                default:
                    break;
            }

            return groupedResult;
        }

        public List<GenreStatisticModel> GetPopularGenreOfOrdersStatistics(DateTime beginDate, DateTime endDate, TimeScale timeScale, IEnumerable<Genre> genres)
        {
            Dictionary<DateTime, List<Order>> ordersByDate = new Dictionary<DateTime, List<Order>>();
            List<GenreStatisticModel> resultList = new List<GenreStatisticModel>();
            List<Order> orderList = GetOrdersBetweenDates(beginDate, endDate);
            int total = 0;

            switch (timeScale)
            {
                case TimeScale.Day:
                    ordersByDate = orderList
                        .GroupBy(g => g.OrderDate.Date)
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key, g => g.ToList());
                    break;
                case TimeScale.Week:
                    ordersByDate = orderList
                        .GroupBy(g => firstDayOfWeek(g.OrderDate))
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key, g => g.ToList());
                    break;
                case TimeScale.Month:
                    ordersByDate = orderList
                        .GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, 1))
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key, g => g.ToList());
                    break;
                default:
                    break;
            }

            foreach (var ordersAtDate in ordersByDate) {
                total = 0;
                var genreAmounts = new Dictionary<String, int>(genres.Count());
                foreach (Genre genre in genres)
                {
                    genreAmounts.Add(genre.Name, 0);
                }

                foreach (Order order in ordersAtDate.Value){
                    foreach (OrderLine line in order.OrderLines){
                        foreach (Genre gGenre in line.Game.Genres)
                        {
                            genreAmounts[gGenre.Name] += line.Amount;
                            total += line.Amount;
                        }
                    }
                }
                resultList.Add(new GenreStatisticModel()
                {
                    Date = ordersAtDate.Key,
                    DateTotal = total,
                    GenreAmounts = genreAmounts,
                    KeyString = "Test"
                });
            }

            return resultList;
        }

        public List<Order> GetOrdersBetweenDates(DateTime beginDate, DateTime endDate)
        {
            var filter = Builders<Order>.Filter.Where(v => v.OrderDate >= beginDate && v.OrderDate < endDate.AddDays(1));
            return Collection.Find(filter).ToList();
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