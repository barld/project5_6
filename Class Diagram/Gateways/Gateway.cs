using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public abstract class Gateway<Model> : IGateway<Model>
    {
        protected readonly string collectionName;
        protected readonly IMongoDatabase connection;

        protected IMongoCollection<Model> Collection => connection.GetCollection<Model>(collectionName);

        protected Gateway(string collectionName, IMongoDatabase connection)
        {
            this.collectionName = collectionName;
            this.connection = connection;
        }

        public async virtual Task<IEnumerable<Model>> GetAll()
        {
            return await Collection.Find(new BsonDocument()).ToListAsync();
        }

        public async virtual Task<Model> GetById(string id)
        {
            var filter = Builders<Model>.Filter.Eq("_id", id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Insert(Model model)
        {
            //Insert new data into a specific collection
            await Collection.InsertOneAsync(model);
        }

        public async Task InsertMany(IEnumerable<Model> collection)
        {
            await Collection.InsertManyAsync(collection);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnToMatch"></param>
        /// <param name="valueToMatch"></param>
        /// <returns></returns>
        public virtual async Task Delete(string columnToMatch, string valueToMatch)
        {
            var filter = Builders<Model>.Filter.Eq(columnToMatch, valueToMatch);
            var result = await Collection.DeleteManyAsync(filter);
        }

        /// <summary>
        /// To replace/update a user, the whole object in the database will be replaced.
        /// </summary>
        /// <param name="model">The model of the collection such as 'User'</param>
        /// <param name="searchField">e.g. 'email' to search for the column 'email'</param>
        /// <param name="searchValue">e.g. 'test@example.com' to find an object with this email</param>
        /// <returns>Returns the result of this operation</returns>
        public async Task Replace(string searchField, string searchValue, Model model)
        {
            var filter = Builders<Model>.Filter.Eq(searchField, searchValue);
            await Collection.ReplaceOneAsync(filter, model);
        }
    }
}