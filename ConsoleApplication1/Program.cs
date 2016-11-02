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

        static void showMenuOptions()
        {
            Console.WriteLine(@"Which example would you like to run?:
1. Reset database
2. Register user account
3. Update user account
4. Add new game
5. Login system
6. Find username by searching the email
7. Display all users
8. Display all games
9. Delete user by email
10. Exit application");
            Console.WriteLine("-----------------------");
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
                case "9":
                    deleteUserAccount();
                    break;
                case "10":
                    Console.WriteLine("Press [ENTER] to exit..");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("No valid choice given, try again...");
                    break;
            }
            Console.WriteLine("-----------------------");
            showMenuOptions();
            Console.WriteLine("-----------------------");
        }

        static async void addNewGame()
        {
            try
            {
                Console.WriteLine("***ADD NEW GAME TO THE DATABASE***");
                Console.WriteLine("Game Title:");
                string GameTitle = Console.ReadLine();
                Console.WriteLine("Game EAN (unique):");
                long GameEAN = Convert.ToInt64(Console.ReadLine());

                Game game = new Game { GameTitle = GameTitle, EAN = GameEAN };
                await context.Games.Insert(game);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not add the new game");
                Console.WriteLine("Error: " + ex.Message);
            }
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

        static void simulateLoginAccount()
        {
            Console.WriteLine("Please login with your user account, you will see at the end whether it was succesfull or not.");
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string pwd = Console.ReadLine();
            User a = context.Users.Login(email, pwd).Result;
            Console.WriteLine($"User logged in status: {a != null}");
        }

        static void retrieveAllUsers()
        {
            Console.WriteLine("Displaying every username...");
            IEnumerable<User> listOfUsers = context.Users.GetAll().Result;

            foreach (User u in listOfUsers)
            {
                Console.WriteLine($"{u.Email} has the following active status: {u.IsActive}");
            }
        }

        static void simulateFindUsernameByEmail()
        {
            try
            {
                Console.WriteLine("Type in the email address to look for:");
                string email = Console.ReadLine();
                var user = context.Users.GetByEmail(email).Result;
                Console.WriteLine($"{user.Email} and the role is: {user.AccountRole}");
            }
            catch
            {
                Console.WriteLine("Could not find the e-mail");
                showMenuOptions();
            }
            
            
        }

        static async void simulateRegisterAccount()
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

        static async void deleteUserAccount()
        {
            try
            {
                Console.WriteLine("What is the e-mail address of the person you're looking for?");
                string email = Console.ReadLine();
                await context.Users.Delete("Email", email);
            }
            catch(Exception ex)
            {
                Console.WriteLine("No accounts have been deleted");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
