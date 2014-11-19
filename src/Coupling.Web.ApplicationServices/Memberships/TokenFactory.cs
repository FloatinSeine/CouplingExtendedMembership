using System.Security.Cryptography;
using System.Web;

namespace Coupling.Web.ApplicationServices.Memberships
{
    internal class TokenFactory
    {
        private const int TokenSizeInBytes = 16;

        public static string Create()
        {
            using (var prng = new RNGCryptoServiceProvider())
            {
                return GenerateToken(prng);
            }
        }

        internal static string GenerateToken(RandomNumberGenerator generator)
        {
            var tokenBytes = new byte[TokenSizeInBytes];
            generator.GetBytes(tokenBytes);
            return HttpServerUtility.UrlTokenEncode(tokenBytes);
        }
    }
}
