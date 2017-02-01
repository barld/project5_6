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
using DataModels.Statistics;

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

        public List<SaleStatisticsJsonDataModel> GetOrderAmountDataTask(TimeScale timeScale, DateTime beginDate, DateTime endDate)
        {
            //timeSpan.d
            CultureInfo ci = CultureInfo.CreateSpecificCulture("nl-NL");
            Thread.CurrentThread.CurrentCulture = ci;

            var result = GetOrdersBetweenDates(beginDate, endDate);
            var calander = ci.DateTimeFormat.FirstDayOfWeek;

            var groupedResult = new List<SaleStatisticsJsonDataModel>();
            switch (timeScale)
            {
                case TimeScale.Day:
                    groupedResult = result
                        .GroupBy(g => g.OrderDate.Date)
                        .Select(grp => new SaleStatisticsJsonDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = grp.Key.Date.ToShortDateString() })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                case TimeScale.Week:
                    groupedResult = result
                        .GroupBy(g => firstDayOfWeek(g.OrderDate))
                        .Select(grp => new SaleStatisticsJsonDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = "W" + ci.Calendar.GetWeekOfYear(grp.Key, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek) + " - " + grp.Key.Year })
                        .OrderBy(x => x.Date)
                        .ToList();
                    break;
                case TimeScale.Month:
                    groupedResult = result
                        .GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, 1))
                        .Select(grp => new SaleStatisticsJsonDataModel() { Date = grp.Key.Date, Amount = grp.Count(), KeyString = ci.DateTimeFormat.GetMonthName(grp.Key.Month) + " - " + grp.Key.Year })
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
            Dictionary<DateTime, GenreStatisticModel> ordersByDate = new Dictionary<DateTime, GenreStatisticModel>();
            List<GenreStatisticModel> resultList = new List<GenreStatisticModel>();
            List<Order> orderList = GetOrdersBetweenDates(beginDate, endDate);
            CultureInfo ci = CultureInfo.CreateSpecificCulture("nl-NL");
            Thread.CurrentThread.CurrentCulture = ci;
            int total = 0;

            switch (timeScale)
            {
                case TimeScale.Day:
                    ordersByDate = orderList
                        .GroupBy(g => g.OrderDate.Date)
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key, g => createGenreStatisticModel(g.Key, g.Key.Date.ToShortDateString(), g.ToList(), genres, new GenreStatisticModel()));
                    break;
                case TimeScale.Week:
                    ordersByDate = orderList
                        .GroupBy(g => firstDayOfWeek(g.OrderDate))
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key, g => createGenreStatisticModel(g.Key, "W" + ci.Calendar.GetWeekOfYear(g.Key, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek) + " - " + g.Key.Year, g.ToList(), genres, new GenreStatisticModel()));
                    break;
                case TimeScale.Month:
                    ordersByDate = orderList
                        .GroupBy(g => new DateTime(g.OrderDate.Year, g.OrderDate.Month, 1))
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key, g=> createGenreStatisticModel(g.Key, ci.DateTimeFormat.GetMonthName(g.Key.Month) + " - " + g.Key.Year, g.ToList(), genres, new GenreStatisticModel()));
                    break;
                default:
                    break;
            }

            return ordersByDate.Values.ToList();
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

        private GenreStatisticModel createGenreStatisticModel (DateTime dt, String keyString, IList<Order> ordersAtDate, IEnumerable<Genre> genres, GenreStatisticModel model)
        {
            int total = 0;
            var genreAmounts = new Dictionary<String, int>(genres.Count());
            foreach (Genre genre in genres)
            {
                genreAmounts.Add(genre.Name, 0);
            }

            foreach (Order order in ordersAtDate)
            {
                foreach (OrderLine line in order.OrderLines)
                {
                    foreach (Genre gGenre in line.Game.Genres)
                    {
                        genreAmounts[gGenre.Name] += line.Amount;
                        total += line.Amount;
                    }
                }
            }
            return new GenreStatisticModel { Date = dt, DateTotal = total, GenreAmounts = genreAmounts, KeyString = keyString};
        }
    }
}