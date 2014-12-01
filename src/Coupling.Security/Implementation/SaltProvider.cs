
using System.Security.Cryptography;
using System.Text;

namespace Coupling.Security.Implementation
{
    public class SaltProvider
    {
        /// <summary>
        /// Creates a random salt value as a string. 
        /// Note that the resulting string will be an array of binary values and not a printable string.
        /// Also it does not convert each byte to a double byte.
        /// </summary>
        /// <param name="length">Length of salt value</param>
        /// <returns>String representation of salt value</returns>
        public static string GenerateSalt(int length)
        {
            var utf16 = new UnicodeEncoding();
            var saltValue = CreateRandomByteArray(length);
            
            return utf16.GetString(saltValue);
        }

        /// <summary>
        /// Create a random number object using the cryptographic random number generator 
        /// </summary>
        /// <param name="length">Length of byte array to fill</param>
        /// <returns>Array filled with random non zero byte values</returns>
        private static byte[] CreateRandomByteArray(int length)
        {
            var rng = RandomNumberGenerator.Create();
            var saltValue = new byte[length];

            rng.GetNonZeroBytes(saltValue);
            return saltValue;
        }
    }
}
