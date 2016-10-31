using MVC.Controller;
using DataModels;
using DataModels;
using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;
using MVCTestApplication;
using MVC.DevelopmentTest.Controller;

namespace MVCTestApplication.Model
{
    class UserModel
    {
        public DatabaseConnection db;

        public void userRegister(User userdata)
        {
            dbInit();
            db.collectionInsert("user", userdata);
        }

        public List<DataModels.User> userSearch(string email)
        {
            dbInit();
            return db.collectionSearchFor<User>("user", "email", email);
        }

        public List<DataModels.User> userAll()
        {
            dbInit();
            return db.collectionRetrieveAll<User>("user");
        }

        public void dbInit()
        {
            string databaseName = "project__5_6";
            db = new DatabaseConnection("mongodb://localhost:27017", databaseName);
            Console.WriteLine("Connecting to database..");
            db.initialize();
            if (db.isConnected)
            {
                Console.WriteLine($"Database is connected to '{databaseName}'..");
            }
            else
            {
                Console.WriteLine($"Could not connect to database {databaseName}");
            }
        }
    }
}
