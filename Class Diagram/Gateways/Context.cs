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
            mongoClient = new MongoClient("mongodb://localhost:27017");
            database = mongoClient.GetDatabase(databaseName);
            createContraints();
            Games = new GameGateway(database);
            Users = new UserGateway(database);
        }

        //Create the constrains for the database
        private void createContraints()
        {
            //All of the defined contraints below must be unique
            var options = new CreateIndexOptions() { Unique = true };

            //EAN attribute must be unique in the Game collection
            var fieldEAN = new StringFieldDefinition<Game>("EAN");
            database.GetCollection<Game>("Game").Indexes.CreateOne(new IndexKeysDefinitionBuilder<Game>().Ascending(fieldEAN), options);

            //Email attribute must be unique in the User collection
            var fieldEmail = new StringFieldDefinition<User>("Email");
            database.GetCollection<User>("User").Indexes.CreateOne(new IndexKeysDefinitionBuilder<User>().Ascending(fieldEmail), options);
        }

        //Clear the database
        public void Reset()
        {
            //Clear the database by deleting the following collections
            List<string> listOfCollections = new List<string>() { "User", "Address", "Platform", "Game" };

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