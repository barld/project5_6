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
                try
                {
                    Process.Start("mongod");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Could not start 'mongod'");
                    Console.WriteLine($"Error: {ex.Message} \nPress [ENTER] to exit..");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                
                System.Threading.Thread.Sleep(1000);
            }

            string databaseName = "project__5_6";
            context = new Context();

            
            Console.WriteLine($"Database is connected to '{databaseName}'..");

            //**To reset/clean the database, uncomment the following:**//
            //context.Reset();

            //**Examples that make use of the gateways that are provided**//
            //simulateRegisterAccount();
            //updateUserAccount();
            addNewGame();
            //simulateLoginAccount("dhbreedeveld@gmail.com", "geheim123");
            //simulateFindUsernameByEmail();
            //retrieveAllUsers();


            Console.WriteLine("Press [ENTER] to exit..");
            Console.ReadLine();
        }

        static async void addNewGame()
        {
            Console.WriteLine("***ADD NEW GAME TO THE DATABASE***");
            Console.WriteLine("Game Title:");
            string GameTitle = Console.ReadLine();
            Game game = new Game { GameTitle = GameTitle, EAN = 122 };
            await context.Games.Insert(game);
        }

        static async void updateUserAccount()
        {
            try
            {
                retrieveAllUsers();
                Console.WriteLine("What is the email of the user that needs to be updated?");
                string oldEmail = Console.ReadLine();
                Console.WriteLine("What is the new email of the user?");
                string newEmail = Console.ReadLine();
                var user = context.Users.GetByEmail(oldEmail).Result;
                user.Email = newEmail;
                await context.Users.Replace("Email", oldEmail, user);
                Console.WriteLine("Update sent, these are the results:");
                retrieveAllUsers();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not update the user, make sure the email exists!");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void simulateLoginAccount(string email, string password)
        {
            User a = context.Users.Login("dhbreedeveld@gmail.com", "geheim123").Result;
            Console.WriteLine($"User logged in status: {a != null}");
        }

        private static void retrieveAllUsers()
        {
            Console.WriteLine("Displaying every username...");
            IEnumerable<User> listOfUsers = context.Users.GetAll().Result;

            foreach (User u in listOfUsers)
            {
                Console.WriteLine(u.Email);
            }
        }

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

        static async private void simulateRegisterAccount()
        {
            //Ask user for account info to register
            Console.WriteLine("---ACCOUNT INFO---");
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Choose your gender:\n1. Male\n2. Female\n3. Unknown");
            Gender gender;
            switch(Console.ReadLine())
            {
                case "1":
                    gender = Gender.Male;
                    break;
                case "2":
                    gender = Gender.Male;
                    break;
                default:
                    gender = Gender.Unknown;
                    break;
            }

            //Add the user to the database
            await context.Users.Register(email, password, gender);
        }
    }
}
