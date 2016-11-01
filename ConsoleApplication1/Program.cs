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
            context = new Context();
            Console.WriteLine("**The following options are all examples that interact with the database**\n**You can copy/paste examples to your own project**\n");
            showMenuOptions();
        }

        private static void showMenuOptions()
        {
            Console.WriteLine("Which example would you like to run?:\n1. Reset database\n2. Register user account\n3. Update user account\n4. Add new game\n5. Login system\n6. Find username by searching the email\n7. Display all users\n8. Display all games");
            switch (Console.ReadLine())
            {
                case "1":
                    //To reset/clean the database
                    //If its not working, make sure the column names are still correct (found in the Reset method)
                    context.Reset();
                    break;
                case "2":
                    simulateRegisterAccount();
                    break;
                case "3":
                    updateUserAccount();
                    break;
                case "4":
                    addNewGame();
                    break;
                case "5":
                    simulateLoginAccount();
                    break;
                case "6":
                    simulateFindUsernameByEmail();
                    break;
                case "7":
                    retrieveAllUsers();
                    break;
                case "8":
                    retrieveAllGames();
                    break;
                default:
                    Console.WriteLine("No valid choice given, try again...");
                    break;
            }
            Console.WriteLine("Would you like to exit the application? Y/N");
            string answer = Console.ReadLine();
            if(answer == "N" || answer == "n")
            {
                showMenuOptions();
            }
            else
            {
                Console.WriteLine("Press [ENTER] to exit..");
                Console.ReadLine();
            }
        }

        static async void addNewGame()
        {
            Console.WriteLine("***ADD NEW GAME TO THE DATABASE***");
            Console.WriteLine("Game Title:");
            string GameTitle = Console.ReadLine();
            Game game = new Game { GameTitle = GameTitle, EAN = 122 };
            await context.Games.Insert(game);
        }

        static void retrieveAllGames()
        {
            Console.WriteLine("Displaying every game..");
            foreach(Game g in context.Games.GetAll().Result)
            {
                Console.WriteLine(g.GameTitle);
            }
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

        private static void simulateLoginAccount()
        {
            Console.WriteLine("Please login with your user account, you will see at the end whether it was succesfull or not.");
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string pwd = Console.ReadLine();
            User a = context.Users.Login(email, pwd).Result;
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

        private static void simulateFindUsernameByEmail()
        {
            Console.WriteLine("Type in the email address to look for:");
            string email = Console.ReadLine();
            var user = context.Users.GetByEmail(email).Result;
            Console.WriteLine($"{user.Email} and the role is: {user.AccountRole}");
            
        }

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
