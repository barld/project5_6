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
using System.Security.Cryptography;
namespace DataModels
{
    class Program
    {
        static Context context;
        //static DatabaseConnection dc;
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                System.Diagnostics.Process.Start("mongod");
                System.Threading.Thread.Sleep(1000);
            }

            string databaseName = "project__5_6";
            //dc = new DatabaseConnection("mongodb://localhost:27017", databaseName);
            //Console.WriteLine("Connecting to database..");
            //dc.initialize();
            context = new Context();

            
            Console.WriteLine($"Database is connected to '{databaseName}'..");
            //context.reset();
            context.Users.Register("barld@gmail.com", "geheim123", Gender.Male);
            User a = context.Users.Login("dhbreedeveld@gmail.com", "geheim123").Result;
            Console.WriteLine($"user is logedin: {a != null}");

            //simulateRegisterAccount();
            //simulateFindUsernameByEmail();
            //retrieveAllUsers();
            //dc.reset();

            Console.WriteLine("Press [ENTER] to exit..");
            Console.ReadLine();
        }

        //private static void retrieveAllUsers()
        //{
        //    Console.WriteLine("Displaying every username...");
        //    List<User> listOfUsers = dc.collectionRetrieveAll<User>("user");

        //    foreach(User u in listOfUsers)
        //    {
        //        Console.WriteLine(u.Email);
        //    }
        //}

        //private static void simulateFindUsernameByEmail()
        //{
        //    Console.WriteLine("Type in the email address to look for:");
        //    string email = Console.ReadLine();
        //    var listOfResults = dc.collectionSearchFor<User>("user", "email", email);
        //    Console.WriteLine($"Found {listOfResults.Count} results for '{email}'");

        //    foreach(var x in listOfResults)
        //    {
        //        //Index 4 is 'isMale' in the collection 'user'
        //        Console.WriteLine($"{x.Email} and username is: {x.Email}");                
        //    }
        //}

        ////simulateRegisterAccount() method creates 1 new user and 1 or more addresses to this user
        //static List<Address> myAddresses;
        ////City, country and postalcode are shared between 'user' and 'address'
        //static string city;
        //static string country;
        //static string postalcode;
        //static private void simulateRegisterAccount()
        //{
        //    //Ask user for account info to register
        //    Console.WriteLine("---ACCOUNT INFO---");
        //    Console.WriteLine("Enter username:");
        //    string userName = Console.ReadLine();
        //    Console.WriteLine("Enter password:");
        //    string password = Console.ReadLine();
        //    Console.WriteLine("Are you a male? True/False");
        //    bool isMale = Convert.ToBoolean(Console.ReadLine());
        //    Console.WriteLine("Enter email:");
        //    string email = Console.ReadLine();
        //    Console.WriteLine();
        //    createAddress();
        //    Console.WriteLine("Add address? True/False");
        //    bool addExtraAddress = Convert.ToBoolean(Console.ReadLine());

        //    //Zolang de user extra addressen wilt toevoegen, roep createAddress() weer aan
        //    while (addExtraAddress)
        //    {
        //        createAddress();
        //        Console.WriteLine("Add extra address? True/False");
        //        addExtraAddress = Convert.ToBoolean(Console.ReadLine());
        //    }

        //    //Create new user
        //    User a = new User() { Email=userName, Password=password, Gender=Gender.Male, AccountRole=AccountRole.User, };

        //    //Add the user to the database
        //    //dc.collectionInsertUser(a);
        //    dc.collectionInsert("user", a);
        //}

        ////This is part of the simulateRegisterAccount() method
        //static private List<Address> createAddress()
        //{
        //    //Create empty addresses list
        //    myAddresses = new List<Address>();

        //    //Ask user for address info
        //    Console.WriteLine("---ADDRESS INFO---");
        //    Console.WriteLine("Enter city:");
        //    city = Console.ReadLine();
        //    Console.WriteLine("Enter country:");
        //    country = Console.ReadLine();
        //    Console.WriteLine("Enter postalcode:");
        //    postalcode = Console.ReadLine();
        //    Console.WriteLine("Enter street:");
        //    string street = Console.ReadLine();
        //    Console.WriteLine("Enter street number:");
        //    int streetnr = Convert.ToInt32(Console.ReadLine());

        //    //Add address info to the empty list
        //    myAddresses.Add(new Address { City=city, Country=country, PostalCode=postalcode, Streetname=street, Housenumber=streetnr});

        //    return myAddresses;
        //}
    }
}
