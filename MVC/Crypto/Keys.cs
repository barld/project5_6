using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Crypto
{
    public static class Keys
    {
        /// <summary>
        /// get a random generated string key
        /// </summary>
        /// <param name="length">length of the return string/key</param>
        /// <returns>a random string safe for security usage</returns>
        public static string GetRandomKey(int length)
        {
            if (length == 0) return string.Empty;
            if (length < 0) throw new ArgumentOutOfRangeException($"paramater {nameof(length)} does not support negative input");

            byte[] randBytes = new byte[length];

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes.Select(b => Convert.ToChar(b)).Aggregate("", (acc, c) => acc+c);
        }
    }
}
