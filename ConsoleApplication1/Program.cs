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
using Class_Diagram.Importers;
using Class_Diagram.Importers.Impl;
using Class_Diagram.Importers.DataContainers;
using static Class_Diagram.Importers.DataContainers.Platforms;

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
                catch (Exception ex)
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
20. Create sample database
21. Import games from external API
22. Filter games by platform
23. Exit application");
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
                createSampleDatabase,
                importGames,
                filterByPlatform,
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

        async static void filterByPlatform()
        {
            Console.WriteLine("Choose a platform (e.g. 2):");
            IEnumerable<Platform> platforms = context.Platforms.GetAll().Result;
            int count = 0;
            foreach(Platform p in platforms)
            {
                count++;
                Console.WriteLine($"{count}. {p.PlatformTitle}");
            }
            int answer = Convert.ToInt16(Console.ReadLine()) - 1;
            Platform platform = platforms.ElementAt(answer);
            IEnumerable<Game> listOfGames = await context.Games.GetAllByPlatform(platform);

            foreach(Game game in listOfGames)
            {
                Console.WriteLine(game.GameTitle);
            }
        }

        static async void createSampleDatabase()
        {
            //Create addresses
            Address address1 = new Address { City = "Rotterdam", Country = "The Netherlands", Housenumber = "99XB", PostalCode = "3099PA", Streetname = "Wijnhaven" };
            Address address2 = new Address { City = "Amsterdam", Country = "The Netherlands", Housenumber = "862", PostalCode = "8629PA", Streetname = "Schiekade" };
            Address address3 = new Address { City = "Den Haag", Country = "The Netherlands", Housenumber = "301", PostalCode = "5182JW", Streetname = "Coolsingel" };
            await context.Addresses.Insert(address1);
            await context.Addresses.Insert(address2);
            await context.Addresses.Insert(address3);

            //Create platforms
            Platform platform1 = new Platform { Brand = "SONY", Description = "Japan for the win!", PlatformTitle = "Playstation 4", Abbreviation = "PS4"  };
            Platform platform2 = new Platform { Brand = "Microsoft", Description = "USA for the win!", PlatformTitle = "Xbox One", Abbreviation= "XBO" };
            Platform platform3 = new Platform { Brand = "Nintendo", Description = "Japan for the win!", PlatformTitle = "Wii U", Abbreviation = "Wii U" };
            Platform platform4 = new Platform { Brand = "Nintendo", Description = "Japan for the win!", PlatformTitle = "3DS", Abbreviation = "3DS" };
            Platform platform5 = new Platform { Brand = "", Description = "World for the win!", PlatformTitle = "PC", Abbreviation = "PC" };
            await context.Platforms.Insert(platform1);
            await context.Platforms.Insert(platform2);
            await context.Platforms.Insert(platform3);
            await context.Platforms.Insert(platform4);
            await context.Platforms.Insert(platform5);

            //Create genres
            Genre genre1 = new Genre { Name = "Action", Description = "Boom boom!" };
            Genre genre2 = new Genre { Name = "Fantasy", Description = "Smexy hihi!" };
            Genre genre3 = new Genre { Name = "RPG", Description = "Every cool nerd wants to live his own digital life!" };
            await context.Genres.InsertMany(new List<Genre> { genre1, genre2, genre3 });

            //Create games
            Game game1 = new Game { GameTitle = "Battlefield 1", Description = "Full of action!", EAN = 754348644, Image = new List<string>() { "https://content.pulse.ea.com/content/battlefield-portal/nl_NL/news/battlefield-1/battlefield-1-beta-thank-you/_jcr_content/featuredImage/renditions/rendition1.img.jpg" }, Publisher = new List<string> { "EA" }, IsVRCompatible = false, MinPlayers = 1, MaxPlayers = 12, Price = 4500, RatingPEGI = 13, ReleaseDate = DateTime.Now, Genres = new List<Genre>{ genre1 }, Platform = platform1 };
            Game game2 = new Game { GameTitle = "Battlefront", Description = "Explosions everywhere!", EAN = 8424785219, Image = new List<string>() { "https://media.starwars.ea.com/content/starwars-ea-com/nl_NL/starwars/battlefront/_jcr_content/ogimage.img.jpeg" }, Publisher = new List<string> { "EA" }, IsVRCompatible = false, MinPlayers = 1, MaxPlayers = 12, Price = 6000, RatingPEGI = 13, ReleaseDate = DateTime.Now, Genres = new List<Genre>{ genre2, genre3 }, Platform = platform2 };
            Game game3 = new Game { GameTitle = "Fifa 2016", Description = "Soccer soccer soccer!", EAN = 3557528864, Image = new List<string>() { "http://image.sambafoot.co.uk/screenshot-fifa-2016-game.jpg" }, Publisher = new List<string> { "EA" }, IsVRCompatible = false, MinPlayers = 1, MaxPlayers = 12, Price = 5000, RatingPEGI = 13, ReleaseDate = DateTime.Now, Genres = new List<Genre>{ genre2, genre1, genre3 }, Platform = platform3 };
            await context.Games.InsertMany(new List<Game> { game1, game2, game3 });

            //Create custom lists
            //Create 1 Wish Lists with 3 games for user1
            MyLists ml1 = new MyLists() { TitleOfList = "Wish List", Games = new List<Game>() { game1, game2, game3 } };

            //Create 1 Wish List with 2 games for user2
            MyLists ml3 = new MyLists() { TitleOfList = "Wish List", Games = new List<Game>() { game3, game1 } };

            //Create 1 Favourite List with 1 game for user1
            MyLists ml2 = new MyLists() { TitleOfList = "Favourite List", Games = new List<Game>() { game3 } };

            //Bundle the custom made lists per user
            List<MyLists> listForUser1 = new List<MyLists>() { ml1, ml2 };
            List<MyLists> listForUser2 = new List<MyLists>() { ml3 };

            //Create users
            User user1 = context.Users.Register("info@superict.nl", "geheim123", Gender.Male).Result;
            user1.MyLists = listForUser1;
            await context.Users.Update(user1);
            User user2 = context.Users.Register("hallo@barld.nl", "geheim321", Gender.Female, AccountRole.Admin).Result;
            user2.MyLists = listForUser2;
            await context.Users.Update(user2);
            User user3 = context.Users.Register("mynameis@jeff.nl", "geheim321", Gender.Unknown).Result;

            //Create order lines
            OrderLine ol1 = new OrderLine { Amount = 1, Game = game1 };
            OrderLine ol2 = new OrderLine { Amount = 5, Game = game2 };
            List<OrderLine> listOfOrderLines1 = new List<OrderLine>() { ol1, ol2 };

            OrderLine ol3 = new OrderLine { Amount = 3, Game = game3 };
            OrderLine ol4 = new OrderLine { Amount = 2, Game = game1 };
            OrderLine ol5 = new OrderLine { Amount = 2, Game = game2 };
            List<OrderLine> listOfOrderLines2 = new List<OrderLine>() { ol3, ol4, ol5 };


            //Create orders that connects the users to different orders, also created 1 user without any orders
            Order order1 = new Order { BillingAddress = address1, Customer = user1, DeliveryAddress = address1, OrderDate = DateTime.Now.AddDays(-1), OrderNumber = context.Orders.GetLatestOrderNumber() + 1, OrderLines = listOfOrderLines1 };
            await context.Orders.Insert(order1);

            Order order2 = new Order { BillingAddress = address2, Customer = user1, DeliveryAddress = address3, OrderDate = DateTime.Now.AddDays(-9), OrderNumber = context.Orders.GetLatestOrderNumber() + 1, OrderLines = listOfOrderLines2 };
            await context.Orders.Insert(order2);

            int orderAmount = 4000;
            Random rand = new Random();
            Order order;

            for(int i = 0; i < orderAmount; i++)
            {
                order = new Order {
                    BillingAddress = address1,
                    Customer = user1,
                    DeliveryAddress = address1,
                    OrderDate = DateTime.Now.AddDays(rand.Next(1,360) - 180),
                    OrderNumber = context.Orders.GetLatestOrderNumber() + 1,
                    OrderLines = listOfOrderLines1
                };
                await context.Orders.Insert(order);
            }


            //Show confirmation message
            Console.WriteLine("Database has been setup successfully!");
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
                Address billingAddress = context.Addresses.GetById(Console.ReadLine()).Result;
                Console.WriteLine("What is the the id of the delivery address?:");
                Address deliveryAddress = context.Addresses.GetById(Console.ReadLine()).Result;

                List<OrderLine> listOfOrderLines = new List<OrderLine>();
                string answer = "y";
                while (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("What is the the id of the game?:");
                    Game game = context.Games.GetById(Console.ReadLine()).Result;
                    Console.WriteLine("What is the amount?");
                    int amount = Convert.ToInt16(Console.ReadLine());
                    listOfOrderLines.Add(new OrderLine { Game = game, Amount = amount });
                    Console.WriteLine("Do you want to add a new game to the order?");
                    answer = Console.ReadLine();
                }
                Order order = new Order { OrderNumber = orderNumber, OrderDate = orderDate, Customer = user, BillingAddress = billingAddress, DeliveryAddress = deliveryAddress, OrderLines = listOfOrderLines };
                await context.Orders.Insert(order);
            }
            catch (Exception ex)
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

            await context.Addresses.Insert(new Address { City = city, Streetname = streetname, Housenumber = houseNumber, Country = country, PostalCode = postalcode });
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
                Platform newPlatform = new Platform { PlatformTitle = platformTitle, Brand = brand, Description = description };
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

                List<string> publisher = new List<string>();
                Console.WriteLine("Publisher:");
                publisher.Add(Console.ReadLine());
                answer = "y";
                while (answer == "y" || answer == "Y")
                {
                    Console.WriteLine("Add another publisher? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "y" || answer == "Y")
                    {
                        Console.WriteLine("Publisher:");
                        publisher.Add(Console.ReadLine());
                    }
                }


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
                    while (answer == "y" || answer == "Y")
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

                Game game = new Game
                {
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
            catch (Exception ex)
            {
                Console.WriteLine("Could not add the new game");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        #endregion

        static void retrieveAllGames()
        {
            Console.WriteLine("Displaying every game..");
            foreach (Game g in context.Games.GetAll().Result)
            {
                Console.WriteLine($"Title: {g.GameTitle} - EAN: {g.EAN} - Price: {g.Price} - Platform: {g.Platform.PlatformTitle}");
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
            catch (Exception ex)
            {
                Console.WriteLine("Could not update the user, make sure the email exists!");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void loginAccount()
        {
            Console.WriteLine("Please login with your user account, you will see at the end whether it was successfull or not.");
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
                Address a = context.Addresses.GetById(id).Result;
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
                Address a = context.Addresses.GetById(id).Result;
                foreach (Order o in context.Orders.GetAllByBillingAddress(a).Result)
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
                        gender = Gender.Female;
                        break;
                    default:
                        gender = Gender.Unknown;
                        break;
                }

                //Add the user to the database
                await context.Users.Register(email, password, gender);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                Console.WriteLine("No accounts have been deleted");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void importGames()
        {
            int amount = 0;
            bool validInput = false; ;
            Console.WriteLine("Please insert the minimum amount of games you want to request from the API");
            Console.WriteLine("-Any value higher than 100 will be discarded and replaced by 100");
            Console.WriteLine("-The amount of games returned can differ dependent on the releases of a game and the completeness of the object returned by the API");
            Console.WriteLine("-This is a long operation as each API call requires a 1.2 second delay.");

            while (!validInput)
            {
                Console.Write("Request minimum amount: ");

                try
                {
                    amount = Int32.Parse(Console.ReadLine());
                    amount = amount > 100 ? 100 : amount;
                    validInput = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("** The input was not valid, please try again. **");
                }
            }

            GameImporter gameImporter = new IGDBGameImporter();
            Platforms.addDesiredPlatform(PlatformId.PC);
            Platforms.addDesiredPlatform(PlatformId.PS4);
            Platforms.addDesiredPlatform(PlatformId.XBO);
            Platforms.addDesiredPlatform(PlatformId.WIU);
            Platforms.addDesiredPlatform(PlatformId._3DS);
            var result = gameImporter.importGames(amount);

            Console.WriteLine("Imported " + result.Count + "games");
            Console.WriteLine("List of game names:");
            foreach(Game game in result)
            {
                Console.Write(game.EAN + " ");
                Console.Write(game.GameTitle + " ");
                Console.Write(game.Platform.PlatformTitle + " ");
            }

            context.Games.InsertMany(result).RunSynchronously();
        }
    }
}
