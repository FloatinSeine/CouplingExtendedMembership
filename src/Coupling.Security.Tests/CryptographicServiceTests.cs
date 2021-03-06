﻿using System;
using Coupling.Security.Implementation;
using NUnit.Framework;

namespace Coupling.Security.Tests
{
    [TestFixture]
    public class CryptographicServiceTests
    {
        [Test]
        public void EncryptClearTest_ReturnsEncodedString()
        {
            const string clear = "TooManySecrets";
            const string encText = "tekaZ6H2yJ/k1hnuKbwBJH3WNqxoUEZNIOIxzR0EV5E=";
            const string salt = "salt";

            var srv = new CryptographyService();
            var enc = srv.Encrypt(clear, salt);

            Assert.AreEqual(encText, enc);
        }

        [Test]
        public void EncryptClearText_CanBeReEncodedWithSameSalt()
        {
            const string clear = "TooManySecrets";
            const string salt = "salt";

            var srv = new CryptographyService();
            var encA = srv.Encrypt(clear, salt);
            var encB = srv.Encrypt(clear, salt);

            Assert.AreEqual(encA, encB);
        }

        [Test]
        public void EncryptClearTest_AutoGeneratedSalt_ReturnsEncodedStringContainingSalt()
        {
            const string clear = "TooManySecrets";
            var srv = new CryptographyService();
            var enc = srv.Encrypt(clear);

            Assert.IsTrue(enc.Contains("#-#"));
        }

        [Test]
        public void EncryptTextContainingSalt_AllowsReEncoding_ReturnsTrueMatching()
        {
            const string clear = "TooManySecrets";
            var srv = new CryptographyService();
            var enc = srv.Encrypt(clear);

            var splitIdx = enc.IndexOf("#-#", StringComparison.InvariantCulture);
            var salt = enc.Substring(0, splitIdx);
            var encText = enc.Substring(splitIdx + 3);
            
            srv = new CryptographyService();
            var newEnc = srv.Encrypt(clear, salt);

            Assert.AreEqual(enc, salt+"#-#"+newEnc);
        }
    }
}
