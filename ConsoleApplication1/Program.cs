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
            List<Action> actions = new List<Action> {
                context.Reset,
                simulateRegisterAccount,
                updateUserAccount,
                addNewGame,
                simulateLoginAccount,
                simulateFindUsernameByEmail,
                retrieveAllUsers,
                retrieveAllGames,
                deleteUserAccount,
                () => {
                    Console.WriteLine("Press [ENTER] to exit..");
                    Console.ReadLine();
                    Environment.Exit(0); }
            };
            try
            {
                int choice = int.Parse(Console.ReadLine());
                actions[choice - 1]();
            }
            catch { Console.WriteLine("No valid choice given.."); }
            Console.WriteLine("-----------------------");
            showMenuOptions();
            Console.WriteLine("-----------------------");
        }

        #region A game needs a platform and 1 or more genres
        static async Task<Platform> addNewPlatform()
        {
            try
            {
                Console.WriteLine("***ADD NEW PLATFORM TO THE DATABASE***");
                Console.WriteLine("Platform title:");
                string platformTitle = Console.ReadLine();
                Console.WriteLine("Brand:");
                string brand = Console.ReadLine();
                Console.WriteLine("Description:");
                string description = Console.ReadLine();
                Platform platform = new Platform { PlatformTitle=platformTitle, Brand = brand, Description=description};
                await context.Platforms.Insert(platform);
                return platform;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not add the platform");
                Console.WriteLine("Error: " + ex.Message);
            }
            return new Platform();
        }

        private static Genre addNewGenre()
        {
            Console.WriteLine("***ADD NEW GENRE TO THE DATABASE***");
            Console.WriteLine("Name of genre:");
            string name = Console.ReadLine();
            Console.WriteLine("Description of this genre:");
            string description = Console.ReadLine();
            Genre genre = new Genre { Name = name, Description = description };
            return genre;
        }

        static async void addNewGame()
        {
            try
            {
                //Game related
                Console.WriteLine("***ADD NEW GAME TO THE DATABASE***");
                Console.WriteLine("Game Title:");
                string gameTitle = Console.ReadLine();

                //Platform related
                Console.WriteLine("Create new platform? Y/N");
                string answer = Console.ReadLine();
                Platform platform = new Platform();
                if (answer == "y" || answer == "Y")
                {
                    //Create new platform
                    platform = addNewPlatform().Result;
                }
                else
                {
                    //Show existing platforms
                    retrieveAllPlatforms();
                    Console.WriteLine("Type in the title of the platform:");
                    platform = context.Platforms.GetByTitle(Console.ReadLine()).Result;
                }
                

                //Game related
                Console.WriteLine("PEGI Rating:");
                int ratingPEGI = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Publisher:");
                string publisher = Console.ReadLine();

                //Genre related
                Console.WriteLine("Create new genre(s)? Y/N");
                answer = Console.ReadLine();
                List<Genre> genres = new List<Genre>();
                if (answer == "y" || answer == "Y")
                {
                    //Create new genre
                    genres.Add(addNewGenre());
                }
                else
                {
                    //Show existing genres
                    //While the user wants to keep adding genres..add a new genre
                    answer = "y";
                    while(answer == "y" || answer == "Y")
                    {
                        retrieveAllGenres();
                        Console.WriteLine("Type in the title of the genre:");
                        genres.Add(context.Genres.GetByTitle(Console.ReadLine()).Result);
                        Console.WriteLine("Add another genre? Y/N");
                        answer = Console.ReadLine();
                    }
                }

                //Game related
                Console.WriteLine("Image URL:");
                string imageURL = Console.ReadLine();

                Console.WriteLine("Min Players:");
                int minPlayers = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("max Players:");
                int maxPlayers = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Description of the game:");
                string gameDescription = Console.ReadLine();

                Console.WriteLine("Unique Game EAN:");
                long gameEAN = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Is VR Compatible? True/False:");
                bool isVRCompatible = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine("Game Price:");
                int price = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Release date (31/01/2016):");
                DateTime releaseDate = Convert.ToDateTime(Console.ReadLine());

                Game game = new Game {
                    GameTitle = gameTitle,
                    Platform = platform,
                    RatingPEGI = ratingPEGI,
                    Publisher = publisher,
                    Genres = genres,
                    Image = imageURL,
                    MinPlayers = minPlayers,
                    MaxPlayers = maxPlayers,
                    Description = gameDescription,
                    EAN = gameEAN,
                    Price = price,
                    IsVRCompatible = isVRCompatible,
                    ReleaseDate = releaseDate
                };
                await context.Games.Insert(game);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not add the new game");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        #endregion

        static void retrieveAllPlatforms()
        {
            Console.WriteLine("Displaying every platform..");
            foreach (Platform p in context.Platforms.GetAll().Result)
            {
                Console.WriteLine($"Title: {p.PlatformTitle} - Brand: {p.Brand}");
            }
        }

        static void retrieveAllGenres()
        {
            Console.WriteLine("Displaying every genre..");
            foreach (Genre g in context.Genres.GetAll().Result)
            {
                Console.WriteLine($"Title: {g.Name}");
            }
        }

        static void retrieveAllGames()
        {
            Console.WriteLine("Displaying every game..");
            foreach(Game g in context.Games.GetAll().Result)
            {
                Console.WriteLine($"Title: {g.GameTitle} - EAN: {g.EAN} - Price: {g.Price} - Platform: {g.Platform}");
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
            try
            {
                //Ask user for account info to register
                Console.WriteLine("---ACCOUNT INFO---");
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                Console.WriteLine("Choose your gender:\n1. Male\n2. Female\n3. Unknown");
                Gender gender;
                switch (Console.ReadLine())
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
            catch(Exception ex)
            {
                Console.WriteLine("Could not register account, make sure the email does not exist already!");
                Console.WriteLine("Error: " + ex.Message);
            }
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
