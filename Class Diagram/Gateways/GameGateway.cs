using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;
using Class_Diagram;

namespace DataModels.Gateways
{
    public class GameGateway : Gateway<Game>, IGameGateway
    {
        public GameGateway(IMongoDatabase connection) : base("Game", connection)
        {
        }

        /// <summary>
        /// This will search for the title of a game and it will retrieve only 1 'Game' from the results, even if multiple titles were found.
        /// </summary>
        /// <param name="searchTitle">The title of the game</param>
        /// <returns>The first result it matched</returns>
        public async Task<Game> GetByTitle(string searchTitle)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.GameTitle, searchTitle);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Game> GetByEAN(long EAN)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.EAN, EAN);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Update(Game game)
        {
            var filter = Builders<Game>.Filter.Eq(x => x._id, game._id);
            await Collection.ReplaceOneAsync(filter, game);
        }

        public async Task<IEnumerable<Game>> GetByTitleLike(SearchGameModel search )
        {
            var builder = Builders<Game>.Filter;
            FilterDefinition<Game> filter = FilterDefinition<Game>.Empty;
            if(search.Title.Length>0)
                filter &= builder.Regex("GameTitle", new BsonRegularExpression(search.Title, "i"));
            if(search.PriceLt > 0)
                filter &= Builders<Game>.Filter.Lt(g => g.Price, search.PriceLt);
            if (search.PriceGt > 0)
                filter &= Builders<Game>.Filter.Gt(g => g.Price, search.PriceGt);
            if (search.Platforms?.Count() > 0)
                filter &= Builders<Game>.Filter.In(g => g.Platform.PlatformTitle, search.Platforms);
            return await Collection.Find(filter).ToListAsync();
        }

        /// <summary>a
        /// Receives all games in a list that matches the title that was given
        /// </summary>
        /// <param name="searchTitle">The title of the game</param>
        /// <returns>All results that match</returns>
        public async Task<IEnumerable<Game>> GetAllByTitle(string searchTitle)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.GameTitle, searchTitle);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetAllByPlatform(Platform platform)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.Platform, platform);
            return await Collection.Find(filter).ToListAsync();
        }

        public void DeleteOnEan(long ean)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.EAN, ean);
            Collection.DeleteOne(filter);
        }

    }
}