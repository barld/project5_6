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
        //context has access to all gateways
        static Context context;

        //These 2 variables are here to pass the objects from addNewPlatform()/addNewGenre() to addNewGame()
        static Platform tempPlatform = new Platform();
        static Genre tempGenre = new Genre();

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
4. Add new platform
5. Add new genre
6. Add new game
7. Add new order
8. Add new address
9. Login system
10. Find username by searching the email
11. Find order by delivery address
12. Find order by billing address
13. Display all users
14. Display all games
15. Display all platforms
16. Display all genres
17. Display all orders
18. Display all addresses
19. Delete user by email
20. Exit application");
            Console.WriteLine("-----------------------");
            List<Action> actions = new List<Action> {
                context.Reset,
                registerAccount,
                updateUserAccount,
                addNewPlatform,
                addNewGenre,
                addNewGame,
                addNewOrder,
                addNewAddress,
                loginAccount,
                findUsernameByEmail,
                findOrdersByDeliveryAddress,
                findOrdersByBillingAddress,
                retrieveAllUsers,
                retrieveAllGames,
                retrieveAllPlatforms,
                retrieveAllGenres,
                retrieveAllOrders,
                retrieveAllAddresses,
                deleteUserAccountByEmail,
                () => {
                    Console.WriteLine("Press [ENTER] to exit..");
                    Console.ReadLine();
                    Environment.Exit(0); },

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
        
        static async void addNewOrder()
        {
            try
            {
                Console.WriteLine("***ADDING NEW ORDER (EXAMPLE)***");
                Console.WriteLine("Create an unique order number:");
                int orderNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("What is the date of the order?:");
                DateTime orderDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("What is the the email address of the user?:");
                User user = context.Users.GetByEmail(Console.ReadLine()).Result;
                Console.WriteLine("What is the the id of the billing address?:");
                Address billingAddress = context.Addresses.GetById(ObjectId.Parse(Console.ReadLine())).Result;
                Console.WriteLine("What is the the id of the delivery address?:");
                Address deliveryAddress = context.Addresses.GetById(ObjectId.Parse(Console.ReadLine())).Result;

                List<OrderLine> listOfOrderLines = new List<OrderLine>();
                string answer = "y";
                while(answer == "y" || answer == "Y")
                {
                    Console.WriteLine("What is the the id of the game?:");
                    Game game = context.Games.GetById(ObjectId.Parse(Console.ReadLine())).Result;
                    Console.WriteLine("What is the amount?");
                    int amount = Convert.ToInt16(Console.ReadLine());
                    listOfOrderLines.Add(new OrderLine { Game=game, Amount=amount });
                    Console.WriteLine("Do you want to add a new game to the order?");
                    answer = Console.ReadLine();
                }
                Order order = new Order {OrderNumber=orderNumber, OrderDate=orderDate, Customer=user, BillingAddress=billingAddress, DeliveryAddress=deliveryAddress, OrderLines=listOfOrderLines };
                await context.Orders.Insert(order);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not add the order");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static async void addNewAddress()
        {
            Console.WriteLine("***ADDING NEW ADDRESS (EXAMPLE)***");
            Console.WriteLine("What is the city?:");
            string city = Console.ReadLine();
            Console.WriteLine("What is the streetname?:");
            string streetname = Console.ReadLine();
            Console.WriteLine("What is the housenumber?:");
            string houseNumber = Console.ReadLine();
            Console.WriteLine("What is the country?:");
            string country = Console.ReadLine();
            Console.WriteLine("What is the postalcode?:");
            string postalcode = Console.ReadLine();

            await context.Addresses.Insert(new Address { City=city, Streetname=streetname, Housenumber=houseNumber, Country=country, PostalCode=postalcode });
            Console.WriteLine("Address has been added to the database");
        }

        #region A game needs a platform and 1 or more genres
        static async void addNewPlatform()
        {
            try
            {
                Console.WriteLine("***ADD NEW PLATFORM TO THE DATABASE EXAMPLE***");
                Console.WriteLine("Platform title:");
                string platformTitle = Console.ReadLine();
                Console.WriteLine("Brand:");
                string brand = Console.ReadLine();
                Console.WriteLine("Description:");
                string description = Console.ReadLine();
                Platform newPlatform = new Platform { PlatformTitle=platformTitle, Brand = brand, Description=description};
                await context.Platforms.Insert(newPlatform);
                tempPlatform = newPlatform;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not add the platform");
                Console.WriteLine("Error: " + ex.Message);
                tempPlatform = new Platform();
            }
        }

        static async void addNewGenre()
        {
            try
            {
                Console.WriteLine("***ADD NEW GENRE TO THE DATABASE***");
                Console.WriteLine("Name of genre:");
                string name = Console.ReadLine();
                Console.WriteLine("Description of this genre:");
                string description = Console.ReadLine();
                Genre newGenre = new Genre { Name = name, Description = description };
                await context.Genres.Insert(newGenre);
                tempGenre = newGenre;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not add the genre");
                Console.WriteLine("Error: " + ex.Message);
                tempGenre = new Genre();
            }
        }

        static async void addNewGame()
        {
            try
            {
                //Game related
                Console.WriteLine("***ADD NEW GAME TO THE DATABASE EXAMPLE***");
                Console.WriteLine("Game Title:");
                string gameTitle = Console.ReadLine();

                //Platform related
                Console.WriteLine("Create new platform? Y/N");
                string answer = Console.ReadLine();
                Platform platform = new Platform();
                if (answer == "y" || answer == "Y")
                {
                    //Create new platform
                    addNewPlatform();
                    platform = tempPlatform;
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
                    addNewGenre();
                    genres.Add(tempGenre);
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
                answer = "y";
                List<string> imageURL = new List<string>();
                while (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("Image URL:");
                    imageURL.Add(Console.ReadLine());
                    Console.WriteLine("Add another URL? Y/N");
                    answer = Console.ReadLine();
                }

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

        static void retrieveAllGames()
        {
            Console.WriteLine("Displaying every game..");
            foreach(Game g in context.Games.GetAll().Result)
            {
                Console.WriteLine($"Title: {g.GameTitle} - EAN: {g.EAN} - Price: {g.Price} - Platform: {g.Platform}");
            }
        }

        static void retrieveAllOrders()
        {
            Console.WriteLine("Displaying every order..");
            foreach (Order o in context.Orders.GetAll().Result)
            {
                Console.WriteLine($"Order Nr: {o.OrderNumber} was done by: {o.Customer.Email} on {o.OrderDate}");
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

        static void loginAccount()
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

        static void retrieveAllPlatforms()
        {
            Console.WriteLine("Displaying every platform...");
            IEnumerable<Platform> listOfUsers = context.Platforms.GetAll().Result;

            foreach (Platform u in listOfUsers)
            {
                Console.WriteLine($"{u.PlatformTitle} has the following brand: {u.Brand}");
            }
        }

        static void retrieveAllGenres()
        {
            Console.WriteLine("Displaying every genre...");
            IEnumerable<Genre> listOfUsers = context.Genres.GetAll().Result;

            foreach (Genre u in listOfUsers)
            {
                Console.WriteLine($"{u.Name}");
            }
        }

        static void retrieveAllAddresses()
        {
            Console.WriteLine("Displaying all addresses...");
            foreach (var address in context.Addresses.GetAll().Result)
            {
                Console.WriteLine($"id: {address._id} and the city is: {address.City}");
            }
        }

        static void findUsernameByEmail()
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

        static void findOrdersByDeliveryAddress()
        {
            try
            {
                Console.WriteLine("Type in the _id of the address that you want to look for:");
                string id = Console.ReadLine();
                Address a = context.Addresses.GetById(ObjectId.Parse(id)).Result;
                foreach (Order o in context.Orders.GetAllByDeliveryAddress(a).Result)
                {
                    Console.WriteLine($"Order Nr: {o.OrderNumber} was done by: {o.Customer.Email}");
                }
            }
            catch
            {
                Console.WriteLine("Could not find any orders..make sure the address id was correct");
                showMenuOptions();
            }
        }

        static void findOrdersByBillingAddress()
        {
            try
            {
                Console.WriteLine("Type in the _id of the address that you want to look for:");
                string id = Console.ReadLine();
                Address a = context.Addresses.GetById(ObjectId.Parse(id)).Result;
                foreach(Order o in context.Orders.GetAllByBillingAddress(a).Result)
                {
                    Console.WriteLine($"Order Nr: {o.OrderNumber} was done by: {o.Customer.Email}");
                }
            }
            catch
            {
                Console.WriteLine("Could not find any orders..make sure the address id was correct");
                showMenuOptions();
            }
        }

        

        static async void registerAccount()
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

        static async void deleteUserAccountByEmail()
        {
            try
            {
                Console.WriteLine("***DELETING USER ACCOUNT EXAMPLE***");
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
