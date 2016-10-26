using DataModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DatabaseConnection : IDatabaseConnection
    {
        public override bool isConnected { get; set; }
        public override bool isGeneratedWithSampleData { get; set; }

        IMongoDatabase db;
        string connectionString;
        string databaseName;
        public DatabaseConnection(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
        }

        public override void initialize()
        {
            //Connect to the mongodb server named 'dev1'
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            MongoClient mongoClient = new MongoClient(settings);
            db = mongoClient.GetDatabase(databaseName);
            isConnected = checkConnection();
        }

        public override bool checkConnection()
        {
            if(!isGeneratedWithSampleData)
            {
                reset();
                return true;
            }
            else
            {
                isConnected = false;
                return false;
            }
        }

        public override void reset()
        {
            List<IMongoCollection<BsonDocument>> collectionsToClear = new List<IMongoCollection<BsonDocument>>();
            collectionsToClear.Add(db.GetCollection<BsonDocument>("user"));
            collectionsToClear.Add(db.GetCollection<BsonDocument>("useraddress"));
            collectionsToClear.Add(db.GetCollection<BsonDocument>("category"));
            collectionsToClear.Add(db.GetCollection<BsonDocument>("game"));

            foreach (IMongoCollection<BsonDocument> collection in collectionsToClear)
            {
                var filter = new BsonDocument();
                var result = collection.DeleteManyAsync(filter);
            }
            Debug.WriteLine("Database cleared..");
            isGeneratedWithSampleData = false;
        }

        public override void setupSampleData()
        {
            try
            {
                //Example data
                List<UserAddress> myAddresses = new List<UserAddress>
                {
                    new UserAddress { city = "Rotterdam", isDeliveryAddress = true },
                    new UserAddress { city = "Den Haag", isDeliveryAddress = false }
                };

                User valueUser = new User { userName = "Daniel Meer", password = "TestPW123", addresses = myAddresses };
                UserAddress valueUserAddress = new UserAddress();
                Category valueCategory = new Category();
                Game valueGame = new Game
                {
                    title = "Fifa 2016",
                    platforms = new List<Category> { new Category { platformTitle = "Playstation 4", brand = "Sony", description = "Playstation made by Sony" } },
                    publisher = "EA",
                    year = 2016
                };
                var collectionUser = db.GetCollection<User>("user");
                var collectionUserAddress = db.GetCollection<UserAddress>("useraddress");
                var collectionCategory = db.GetCollection<Category>("category");
                var collectionGame = db.GetCollection<Game>("game");
                collectionUser.InsertOne(valueUser);
                collectionUserAddress.InsertOne(valueUserAddress);
                collectionCategory.InsertOne(valueCategory);
                collectionGame.InsertOne(valueGame);
                Debug.WriteLine("Database generated!");
                isGeneratedWithSampleData = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("Database connection failed, make sure the database is running!");
                isGeneratedWithSampleData = false;
            }
        }
    }
}
