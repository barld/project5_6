using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataModels.Statistics;

namespace DataModels.Gateways
{
    public class UserGateway : Gateway<User>, IUserGateway
    {
        public UserGateway(IMongoDatabase database) : base("User", database)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            var result = await base.GetAll();
            return result.Where(u => u.IsActive);
        }

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        string Sha256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        public async Task<List<MyLists>> GetMyLists(User user)
        {
            if (user != null)
            {
                return user.MyLists;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await GetByEmail(email);
            if (user != null)
            {
                if (user.IsActive && Sha256(user.Salt + password) == user.Password)
                {
                    return user;
                }
            }
            return null;
        }

        private string GetRandomPasswordSalt()
        {
            byte[] randBytes;

            randBytes = new byte[6];

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes.Select(b => Convert.ToChar(b)).Aggregate("", (acc, c) => acc + c);
        }

        public async Task<User> Register(string email, string pwd, Gender gender, List<MyLists> lists = null, AccountRole role = AccountRole.User)
        {
            if (lists == null)
                lists = new List<MyLists>();
            string salt = GetRandomPasswordSalt();
            string hash = Sha256(salt + pwd);
            var user = new User
            {
                AccountRole = role,
                Email = email,
                Gender = gender,
                Password = hash,
                Salt = salt,
                MyLists = lists
            };
            await this.Insert(user);

            return await GetByEmail(email);
        }

        public Task Update(User updatedUser)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x._id, updatedUser._id);
                return Collection.ReplaceOneAsync(filter, updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public override async Task Delete(string columnToMatch, string valueToMatch)
        {
            try
            {
                var user = GetById(valueToMatch).Result;
                user.IsActive = false;
                await Replace(columnToMatch, valueToMatch, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not delete user, make sure the email was correct");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void Delete(User user)
        {
            this.Delete("_id", user._id).Wait();
        }

        public IEnumerable<GameWishListStatisticsJsonDataModel> GetGameWishListStatistics(int amount, ICollection<Genre> genre)
        {
            var filter = Builders<User>.Filter.Eq(x => x.IsActive, true);
            var result = Collection.Find(filter).ToList();
            var wishLists = result
                .SelectMany(x => x.MyLists)
                .Where(y => y.TitleOfList == "Wish List")
                .ToList();

            var gameDictionary = new Dictionary<string, GameWishListStatisticsJsonDataModel>();

            wishLists
                .ForEach(x => x.Games
                    .Where(y => (genre != null) ? gameHasFilteredGenre(y, genre) : true)
                    .ToList()
                    .ForEach(ga =>
                        {
                            if (gameDictionary.ContainsKey(ga.GameTitle))
                                gameDictionary[ga.GameTitle].Count += 1;
                            else
                            {
                                var g = new GameWishListStatisticsJsonDataModel() { GameTitle = ga.GameTitle, Count = 1 };
                                gameDictionary.Add(g.GameTitle, g);
                            }
                        }));
            var endResult = gameDictionary.Values.ToList().OrderBy(g => g.Count);
            return amount > 0 ? endResult.Take(amount) : endResult;
        }

        private bool gameHasFilteredGenre(Game game, ICollection<Genre> genres)
        {
            var result = false; 
            foreach(var genre in game.Genres)
            {
                if (genres.Contains(genre)){
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}