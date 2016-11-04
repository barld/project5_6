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

        public GameGateway Games { get; }
        public PlatformGateway Platforms { get; }
        public GenreGateway Genres { get; }
        public UserGateway Users { get; }
        public OrderGateway Orders { get; }
        public AddressGateway Addresses { get; }
        public Context()
        {
            string databaseName = "project__5_6";
            mongoClient = new MongoClient("mongodb://localhost:27017");
            database = mongoClient.GetDatabase(databaseName);
            createContraints();
            Games = new Gateways.GameGateway(database);
            Users = new UserGateway(database);
            Platforms = new PlatformGateway(database);
            Genres = new GenreGateway(database);
            Orders = new OrderGateway(database);
            Addresses = new AddressGateway(database);
        }

        //Create the constrains for the database
        private void createContraints()
        {
            //All of the defined contraints below must be unique
            var options = new CreateIndexOptions() { Unique = true };

            //OrderNumber attribute must be unique in the Order collection
            var fieldOrderNumber = new StringFieldDefinition<Order>("OrderNumber");
            database.GetCollection<Order>("Order").Indexes.CreateOne(new IndexKeysDefinitionBuilder<Order>().Ascending(fieldOrderNumber), options);

            //EAN attribute must be unique in the Game collection
            var fieldEAN = new StringFieldDefinition<Game>("EAN");
            database.GetCollection<Game>("Game").Indexes.CreateOne(new IndexKeysDefinitionBuilder<Game>().Ascending(fieldEAN), options);

            //PlatformTitle attribute must be unique in the Platform collection
            var fieldPlatformTitle = new StringFieldDefinition<Platform>("PlatformTitle");
            database.GetCollection<Platform>("Platform").Indexes.CreateOne(new IndexKeysDefinitionBuilder<Platform>().Ascending(fieldPlatformTitle), options);

            //Email attribute must be unique in the User collection
            var fieldEmail = new StringFieldDefinition<User>("Email");
            database.GetCollection<User>("User").Indexes.CreateOne(new IndexKeysDefinitionBuilder<User>().Ascending(fieldEmail), options);
        }

        //Clear the database
        public void Reset()
        {
            //Clear the database by deleting the following collections
            List<string> listOfCollections = new List<string>() { "User", "Address", "Platform", "Game", "Order" };

            foreach (string collection in listOfCollections)
            {
                database.GetCollection<BsonDocument>(collection).DeleteOne(new BsonDocument());
                database.DropCollection(collection);
            }
            Console.WriteLine("Database cleared..");
        }

        public void Dispose()
        {
            
        }
    }
}