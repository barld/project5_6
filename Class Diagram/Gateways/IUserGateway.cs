﻿using DataModels.Statistics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public interface IUserGateway : IGateway<User>
    {
        Task Delete(string columnToMatch, string valueToMatch);
        Task<User> GetByEmail(string email);
        Task<List<MyLists>> GetMyLists(User user);
        Task<User> Login(string email, string password);
        Task<User> Register(string email, string pwd, Gender gender, AccountRole role = AccountRole.User);
        Task Update(User currentUser);
        void Delete(User user);
        IEnumerable<GameWishListStatisticsJsonDataModel> GetGameWishListStatistics(int amount, ICollection<Genre> genre);
    }
}