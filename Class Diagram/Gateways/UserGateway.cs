using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataModels.Gateways
{
    public class UserGateway : Gateway<User>
    {
        public UserGateway(IMongoDatabase database) : base("user", database)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        static string sha256(string password)
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

        private string getRandomPasswordSalt()
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

        public async void register(string email, string pwd, Gender gender)
        {
            string salt = getRandomPasswordSalt();

            var user = new User
            {
                AccountRole = AccountRole.User,
                Email = email,
                Gender = gender,
                // TODO: hashing
                Password = pwd + salt,
                Salt = salt,
                MyLists = new List<MyLists>(),
                
            };
            await this.Insert(user);
        }
        
    }
}