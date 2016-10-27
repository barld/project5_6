using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        static DatabaseConnection dc;
        static void Main(string[] args)
        {
            string databaseName = "project__5_6";
            dc = new DatabaseConnection("mongodb://localhost:27017", databaseName);
            Console.WriteLine("Connecting to database..");
            dc.initialize();

            if (dc.isConnected)
            {
                Console.WriteLine($"Database is connected to '{databaseName}'..");
                //simulateRegisterAccount();
                Console.WriteLine("Type in the email address to look for:");
                string email = Console.ReadLine();
                simulateFindEmail(email);
            }
            else
            {
                Console.WriteLine($"Could not connect to database {databaseName}");
            }
            Console.WriteLine("Press [ENTER] to exit..");
            Console.ReadLine();
        }

        private static void simulateFindEmail(string emailAddress)
        {
            var listOfResults = dc.collectionSearchFor("user", "email", emailAddress);
            Console.WriteLine($"Found {listOfResults.Count} results for '{emailAddress}'");

            foreach(var x in listOfResults)
            {
                Console.WriteLine(x.ToString());
            }
        }


        //simulateRegisterAccount() method creates 1 new user and 1 or more addresses to this user
        static List<UserAddress> myAddresses;

        //City, country and postalcode are shared between 'user' and 'address'
        static string city;
        static string country;
        static string postalcode;
        static private void simulateRegisterAccount()
        {
            //Ask user for account info to register
            Console.WriteLine("---ACCOUNT INFO---");
            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Are you a male? True/False");
            bool isMale = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            Console.WriteLine();
            createAddress();
            Console.WriteLine("Add address? True/False");
            bool addExtraAddress = Convert.ToBoolean(Console.ReadLine());

            //Zolang de user extra addressen wilt toevoegen, roep createAddress() weer aan
            while (addExtraAddress)
            {
                createAddress();
                Console.WriteLine("Add extra address? True/False");
                addExtraAddress = Convert.ToBoolean(Console.ReadLine());
            }

            //Create new user
            User a = new User() { country=country, postalCode=postalcode, userName=userName, password=password, isMale=isMale, email=email, addresses=myAddresses};

            //Add the user to the database
            dc.collectionInsertUser(a);
        }

        //This is part of the simulateRegisterAccount() method
        static private List<UserAddress> createAddress()
        {
            //Create empty addresses list
            myAddresses = new List<UserAddress>();

            //Ask user for address info
            Console.WriteLine("---ADDRESS INFO---");
            Console.WriteLine("Enter city:");
            city = Console.ReadLine();
            Console.WriteLine("Enter country:");
            country = Console.ReadLine();
            Console.WriteLine("Enter postalcode:");
            postalcode = Console.ReadLine();
            Console.WriteLine("Enter street:");
            string street = Console.ReadLine();
            Console.WriteLine("Enter street number:");
            int streetnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Is this you delivery address? True/False");
            bool isDeliveryAddress = Convert.ToBoolean(Console.ReadLine());

            //Add address info to the empty list
            myAddresses.Add(new UserAddress { city=city, country=country, postal_code=postalcode, street=street, snumber=streetnr, isDeliveryAddress=isDeliveryAddress });

            return myAddresses;
        }
    }
}
