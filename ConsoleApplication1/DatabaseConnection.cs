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
        public string collectionName { get; }

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
        }

        public void collectionInsert<T>(string collectionName, T model)
        {
            //Insert new data into a specific collection
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                collection.InsertOne(model);
                Console.WriteLine($"{collectionName} has been added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add {collectionName}..");
                Console.WriteLine(ex.Message);
            }
        }

        public List<T> collectionSearchFor<T> (string collectionName, string columnName, string searchTermValue)
        {
            //Search for a specific value and retrieve a list with the collection(s) back
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq(columnName, searchTermValue);
                var result = collection.Find(filter).ToList();
                if(result.Count > 0)
                {
                    Console.WriteLine("***Found some results!***");
                }
                else
                {
                    Console.WriteLine("***No results found!***");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve results for {searchTermValue}..");
                Console.WriteLine(ex.Message);
                return new List<T>();
            }
        }

        public List<T> collectionRetrieveAll<T> (string collectionName)
        {
            //This will retrieve all data from a collection back into a list, without filtering the data first.
            try
            {
                var collection = db.GetCollection<T>(collectionName);
                Console.WriteLine($"***All results from '{collectionName}' has been retrieved!***");
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"***Failed to add {collectionName}..***");
                Console.WriteLine(ex.Message);
                return new List<T>();
            }
        }
    }
}