using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModels.Gateways;
using MongoDB.Bson;

namespace DataModels
{
    public class Context : IContext, IDisposable
    {
        private readonly IMongoDatabase database;
        private readonly MongoClient mongoClient;

        public Context()
        {
            string databaseName = "project__5_6";
            //MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
            mongoClient = new MongoClient("mongodb://localhost:27017");
            database = mongoClient.GetDatabase(databaseName);
            createContraints();
            Games = new GameGateway(database);
            Users = new UserGateway(database);
        }

        //Create the constrains for the database
        private void createContraints()
        {
            var options = new CreateIndexOptions() { Unique = true };
            var fieldUsername = new StringFieldDefinition<User>("userName");
            var indexDefinitionUsername = new IndexKeysDefinitionBuilder<User>().Ascending(fieldUsername);
            var fieldEmail = new StringFieldDefinition<User>("email");
            var indexDefinitionEmail = new IndexKeysDefinitionBuilder<User>().Ascending(fieldEmail);
            database.GetCollection<User>("user").Indexes.CreateOne(indexDefinitionUsername, options);
            database.GetCollection<User>("user").Indexes.CreateOne(indexDefinitionEmail, options);
        }

        //Clear the database
        public void reset()
        {
            //Clear the database by deleting the following collections
            List<string> listOfCollections = new List<string>() { "user", "useraddress", "category", "game" };

            foreach (string collection in listOfCollections)
            {
                database.GetCollection<BsonDocument>(collection).DeleteOne(new BsonDocument());
                database.DropCollection(collection);
            }
            Console.WriteLine("Database cleared..");
        }

        public GameGateway Games { get; }
        public UserGateway Users { get; }
        public void Dispose()
        {
            
        }
    }
}