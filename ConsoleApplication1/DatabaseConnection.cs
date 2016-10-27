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
            this.databaseName = databaseName;;
        }

        public override void initialize()
        {
            //Credentials for the mongodb server
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            MongoClient mongoClient = new MongoClient(settings);
            db = mongoClient.GetDatabase(databaseName);
            var collection = db.GetCollection<User>("user");
            checkConnection(); //Sets the property to either true or false, so you can check the connection
            createContraints(); //Sets the rules for every collection such as unique usernames, unique emails etc
        }

        private void createContraints()
        {
            if(isConnected)
            {
                var options = new CreateIndexOptions() { Unique = true };
                var field = new StringFieldDefinition<User>("userName");
                var indexDefinition = new IndexKeysDefinitionBuilder<User>().Ascending(field);
                db.GetCollection<User>("user").Indexes.CreateOne(indexDefinition, options);
            }
        }

        public override void checkConnection()
        {
            //Try to insert sample data, the database is online if this succeeded
            int retry = 50;
            while(retry != 0)
            {
                System.Threading.Thread.Sleep(100);
                retry--;
                if (db.Client.Cluster.Description.State.ToString() != "Disconnected")
                {

                    //Database connection succeeded
                    isConnected = true;

                    //Clean the database
                    reset();
                    break;
                }
                else
                {
                    //Database connection failed
                    isConnected = false;
                }
            }
        }

        public override void reset()
        {
            List<string> listOfCollections = new List<string>() { "user", "useraddress", "category", "game" };
            
            foreach(string collection in listOfCollections)
            {
                db.GetCollection<BsonDocument>(collection).DeleteOne(new BsonDocument());
                db.DropCollection(collection);
            }

            Debug.WriteLine("Database cleared..");
            isGeneratedWithSampleData = false;
        }

        public void insertUser(User user)
        {
            try
            {
                var collectionUser = db.GetCollection<User>("user");
                collectionUser.InsertOne(user);
                Console.WriteLine("User has been added!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to add user..");
                Console.WriteLine(ex.Message);
            }
        }

        public override bool setupSampleData()
        {
            //Insert sample data
            try
            {
                //Example data
                List<UserAddress> myAddresses = new List<UserAddress>
                {
                    new UserAddress { city = "Rotterdam", isDeliveryAddress = true },
                    new UserAddress { city = "Den Haag", isDeliveryAddress = false }
                };
                User valueUser1 = new User { userName = "Daniel Meer", password = "TestPW123", addresses = myAddresses };
                User valueUser2 = new User { userName = "Daniel Meer", password = "alskd", addresses = myAddresses };
                User valueUser3 = new User { userName = "Daniel Meer", password = "testst", addresses = myAddresses };
                User valueUser4 = new User { userName = "Andy Meer", password = "alskd", addresses = myAddresses };

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
                collectionUser.InsertOne(valueUser1);
                collectionUser.InsertOne(valueUser2);
                collectionUserAddress.InsertOne(valueUserAddress);
                collectionCategory.InsertOne(valueCategory);
                collectionGame.InsertOne(valueGame);
                Debug.WriteLine("Database generated!");
                isGeneratedWithSampleData = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine("Database connection failed, make sure the database is running!");
                isGeneratedWithSampleData = false;

                //It went wrong while inserting the data, delete everything until the problem has been fixed
                //e.g. remove duplicate unique indexes, check the log
                reset();
                return false;
            }
        }
    }
}
