using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.Gateways;

namespace WebshopTests.ContextMock
{
    class MockContext : IContext
    {
        public MockContext(bool fillWithSampleData = false)
        {
            Address address1 = new Address { City = "Rotterdam", Country = "The Netherlands", Housenumber = "99XB", PostalCode = "3099PA", Streetname = "Wijnhaven" };
            Address address2 = new Address { City = "Amsterdam", Country = "The Netherlands", Housenumber = "862", PostalCode = "8629PA", Streetname = "Schiekade" };
            Address address3 = new Address { City = "Den Haag", Country = "The Netherlands", Housenumber = "301", PostalCode = "5182JW", Streetname = "Coolsingel" };

            //Create platforms
            Platform platform1 = new Platform { Brand = "SONY", Description = "Japan for the win!", PlatformTitle = "Playstation 4", Abbreviation = "PS4" };
            Platform platform2 = new Platform { Brand = "Microsoft", Description = "USA for the win!", PlatformTitle = "Xbox One", Abbreviation = "XBO" };
            Platform platform3 = new Platform { Brand = "Nintendo", Description = "Japan for the win!", PlatformTitle = "Wii U", Abbreviation = "Wii U" };
            Platform platform4 = new Platform { Brand = "Nintendo", Description = "Japan for the win!", PlatformTitle = "3DS", Abbreviation = "3DS" };
            Platform platform5 = new Platform { Brand = "", Description = "World for the win!", PlatformTitle = "PC", Abbreviation = "PC" };

            //Create genres
            Genre genre1 = new Genre { Name = "Action", Description = "Boom boom!" };
            Genre genre2 = new Genre { Name = "Fantasy", Description = "Smexy hihi!" };
            Genre genre3 = new Genre { Name = "RPG", Description = "Every cool nerd wants to live his own digital life!" };

            //Create games
            Game game1 = new Game { GameTitle = "Battlefield 1", Description = "Full of action!", EAN = 754348644, Image = new List<string>() { "https://content.pulse.ea.com/content/battlefield-portal/nl_NL/news/battlefield-1/battlefield-1-beta-thank-you/_jcr_content/featuredImage/renditions/rendition1.img.jpg" }, Publisher = new List<string> { "EA" }, IsVRCompatible = false, MinPlayers = 1, MaxPlayers = 12, Price = 4500, RatingPEGI = 13, ReleaseDate = DateTime.Now, Genres = new List<Genre> { genre1 }, Platform = platform1 };
            Game game2 = new Game { GameTitle = "Battlefront", Description = "Explosions everywhere!", EAN = 8424785219, Image = new List<string>() { "https://media.starwars.ea.com/content/starwars-ea-com/nl_NL/starwars/battlefront/_jcr_content/ogimage.img.jpeg" }, Publisher = new List<string> { "EA" }, IsVRCompatible = false, MinPlayers = 1, MaxPlayers = 12, Price = 6000, RatingPEGI = 13, ReleaseDate = DateTime.Now, Genres = new List<Genre> { genre2, genre3 }, Platform = platform2 };
            Game game3 = new Game { GameTitle = "Fifa 2016", Description = "Soccer soccer soccer!", EAN = 3557528864, Image = new List<string>() { "http://image.sambafoot.co.uk/screenshot-fifa-2016-game.jpg" }, Publisher = new List<string> { "EA" }, IsVRCompatible = false, MinPlayers = 1, MaxPlayers = 12, Price = 5000, RatingPEGI = 13, ReleaseDate = DateTime.Now, Genres = new List<Genre> { genre2, genre1, genre3 }, Platform = platform3 };



            var games = new Game[] { game1, game2, game3 };
            if(fillWithSampleData)
            {
                Games = new MockGameGateWay(games);
            }
            
        }

        public IGameGateway Games { get; }

        public PlatformGateway Platforms { get { throw new NotImplementedException(); } }

        public UserGateway Users { get { throw new NotImplementedException(); } }
    }
}
