using Coupling.Web.ApplicationServices.Implementation.Cryptography;
using NUnit.Framework;

namespace Coupling.Web.ApplicationServices.Tests.Cryptography
{
    [TestFixture]
    public class SaltProviderTests
    {
        [Test]
        public void MultipleTokenRequests_ReturnDifferentTokens()
        {
            var tokenA = SaltProvider.GenerateSalt(16);
            var tokenB = SaltProvider.GenerateSalt(16);

            Assert.AreNotEqual(tokenA, tokenB);
        }
    }
}
