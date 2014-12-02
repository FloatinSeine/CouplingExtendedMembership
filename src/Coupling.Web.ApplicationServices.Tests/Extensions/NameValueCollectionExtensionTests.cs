using System.Collections.Specialized;
using NUnit.Framework;
using Coupling.Web.ApplicationServices.Extensions;

namespace Coupling.Web.ApplicationServices.Tests.Extensions
{
    [TestFixture]
    public class NameValueCollectionExtensionTests
    {
        private NameValueCollection _testSet;

        [TestFixtureSetUp]
        public void Setup()
        {
            var nvc = new NameValueCollection();
            nvc.Add("String", "Test");
            nvc.Add("EmptyString", string.Empty);
            nvc.Add("Integer", "1");
            nvc.Add("Boolean", "True");

            _testSet = nvc;
        }

        [Test]
        public void GivenIndexString_ContainsStringTest_ReturnsTest()
        {
            var expected = "Test";
            var result = _testSet.GetValue("String", "default");

            Assert.IsTrue(result.Equals(expected));
        }

        [Test]
        public void GivenIndexEmptyString_ContainsEmptyString_ReturnsDefaultString()
        {
            var expected = "default";
            var result = _testSet.GetValue("EmptyString", expected);

            Assert.IsTrue(result.Equals(expected));
        }

        [Test]
        public void GivenIndexInteger_ContainsString1_Returns1()
        {
            var expected = 1;
            var result = _testSet.GetValue("Integer", 3);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenIndexEmptyString_ContainsEmptyString_ReturnsDefaultInteger()
        {
            var expected = 3;
            var result = _testSet.GetValue("EmptyString", 3);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenIndexBoolean_ContainsStringTrue_ReturnsTrue()
        {
            var expected = true;
            var result = _testSet.GetValue("Boolean", false);

            Assert.IsTrue(expected == result);
        }

        [Test]
        public void GivenIndexEmptyString_ContainsEmptyString_ReturnsDefaultBoolean()
        {
            var expected = false;
            var result = _testSet.GetValue("EmptyString", false);

            Assert.IsTrue(expected == result);
        }
    }
}
