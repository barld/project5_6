using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public class GameGateway : Gateway<Game>
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

        /// <summary>
        /// Receives all games in a list that matches the title that was given
        /// </summary>
        /// <param name="searchTitle">The title of the game</param>
        /// <returns>All results that match</returns>
        public async Task<IEnumerable<Game>> GetAllByTitle(string searchTitle)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.GameTitle, searchTitle);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}