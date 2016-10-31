﻿using System;
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
            //context.reset();

            //**Examples that make use of the gateways that are provided**//
            //simulateRegisterAccount();
            simulateLoginAccount("dhbreedeveld@gmail.com", "geheim123");
            //simulateFindUsernameByEmail();
            //retrieveAllUsers();

            Console.WriteLine("Press [ENTER] to exit..");
            Console.ReadLine();
        }

        private static void simulateLoginAccount(string email, string password)
        {
            User a = context.Users.Login("dhbreedeveld@gmail.com", "geheim123").Result;
            Console.WriteLine($"user is logedin: {a != null}");
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

        static private void simulateRegisterAccount()
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
            bool isMale = Convert.ToBoolean(Console.ReadLine());

            //Add the user to the database
            context.Users.Register(email, password, gender);
        }
    }
}
