using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
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
            return await Collection.Find(new BsonDocument { }).ToListAsync();
        }

        public async virtual Task<Model> GetById(ObjectId id)
        {
            var filter = Builders<Model>.Filter.Eq("_id", id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}