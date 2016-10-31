using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Context : IContext, IDisposable
    {
        private readonly IMongoDatabase connection;
        private readonly MongoClient mongoClient;

        public Context()
        {
            string databaseName = "project__5_6";
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
            mongoClient = new MongoClient(settings);
            connection = mongoClient.GetDatabase(databaseName);

            Games = new GameGateway(connection);
            Users = new UserGateway(connection);
        }

        public GameGateway Games { get; }
        public UserGateway Users { get; }

        public void Dispose()
        {
            
        }
    }
}