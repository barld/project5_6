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
    public abstract class Gateway<Model>
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

        public async virtual Task<Model> GetById(ObjectId id)
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
    }
}