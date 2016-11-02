using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public class GenreGateway : Gateway<Genre>
    {
        public GenreGateway(IMongoDatabase connection) : base("Genre", connection)
        {
        }

        /// <summary>
        /// This will search for the title of a game and it will retrieve only 1 'Game' from the results, even if multiple titles were found.
        /// </summary>
        /// <param name="searchTitle">The title of the game</param>
        /// <returns>The first result it matched</returns>
        public async Task<Genre> GetByTitle(string searchTitle)
        {
            var filter = Builders<Genre>.Filter.Eq(g => g.Name, searchTitle);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Receives all games in a list that matches the title that was given
        /// </summary>
        /// <param name="searchTitle">The title of the game</param>
        /// <returns>All results that match</returns>
        public async Task<IEnumerable<Genre>> GetAllByTitle(string searchTitle)
        {
            var filter = Builders<Genre>.Filter.Eq(g => g.Name, searchTitle);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}