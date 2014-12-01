using Coupling.Security.Implementation;
using NUnit.Framework;

namespace Coupling.Security.Tests
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
