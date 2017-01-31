using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Class_Diagram;
using DataModels;
using DataModels.Gateways;
using System.Linq;

namespace WebshopTests.ContextMock
{
    internal class MockGameGateWay : IGameGateway
    {
        List<Game> games;
        internal MockGameGateWay(IEnumerable<Game> games)
        {
            this.games = games.ToList();
        }
        internal MockGameGateWay()
        {
            games = new List<Game>();
        }

        public async Task<IEnumerable<Game>> GetAllByPlatform(Platform platform) => 
            games.Where(g => g.Platform._id == platform._id);

        public async Task<IEnumerable<Game>> GetAllByTitle(string searchTitle) =>
            games.Where(g => g.GameTitle.Contains(searchTitle));


        public async Task<Game> GetByEAN(long EAN) =>
            games.Find(g => g.EAN == EAN);

        public async Task<Game> GetByTitle(string searchTitle) =>
            games.Find(g => g.GameTitle == searchTitle);

        public async Task<IEnumerable<Game>> GetByTitleLike(SearchGameModel search)
        { throw new NotImplementedException(); }

        public Task Delete(string columnToMatch, string valueToMatch)
        { throw new NotImplementedException(); }

        public async Task<IEnumerable<Game>> GetAll() => games.ToList();

        public async Task<Game> GetById(string id) => games.Find(g => g._id == id);

        public Task Insert(Game model)
        { throw new NotImplementedException(); }

        public Task InsertMany(IEnumerable<Game> collection)
        { throw new NotImplementedException(); }

        public Task Replace(string searchField, string searchValue, Game model)
        { throw new NotImplementedException(); }

        public Task Update(Game game)
        { throw new NotImplementedException(); }
    }
}