using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using DataModels.Gateways;
using System.Linq;

namespace WebshopTests.ContextMock
{
    internal class MockUserGateWay : IUserGateway
    {
        private List<User> testUsers;

        public MockUserGateWay(IEnumerable<User> testUsers)
        {
            this.testUsers = testUsers.ToList();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string columnToMatch, string valueToMatch)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return testUsers.AsEnumerable();
        }

        public async Task<User> GetByEmail(string email)
        {
            return testUsers.Find(user => user.Email == email);
        }

        public async Task<User> GetById(string id)
        {
            return testUsers.Find(user => user._id == id);
        }

        public async Task<List<MyLists>> GetMyLists(User user)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(User model)
        {
            testUsers.Add(model);
        }

        public async Task InsertMany(IEnumerable<User> collection)
        {
            testUsers.AddRange(collection);
        }

        public async Task<User> Login(string email, string password)
        {
            return testUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public async Task<User> Register(string email, string pwd, Gender gender, List<MyLists> lists = null, AccountRole role = AccountRole.User)
        {
            var user = new User { Email = email, Password = pwd, Gender = gender, MyLists = lists, AccountRole = role };

            testUsers.Add(user);
            return user;
        }

        public Task Replace(string searchField, string searchValue, User model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMyLists(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMyLists(User currentUser, string titleOfList, Game game)
        {
            throw new NotImplementedException();
        }
    }
}