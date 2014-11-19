using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Coupling.Web.ApplicationServices.Implementation.Cryptography
{
    public sealed class CryptographyService : IEncrypt
    {
        private const string EncryptionKey = "MAKV2SPBNI99212";
        private const int SaltLength = 32;
        private const int InitVectorSize = 16;  //128bits
        private const int KeySize = 32;         //256bits
        private const int Iterations = 10;

        public string Encrypt(string clearText)
        {
            var salt = SaltProvider.GenerateSalt(SaltLength);
            var enc = Encrypt(clearText, salt);
            return (salt + "#-#" + enc);
        }

        public string Encrypt(string clearText, string salt)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            var saltBytes = Encoding.Unicode.GetBytes(salt);
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, saltBytes, Iterations);

            return AesEncrypt(clearBytes, pdb);
        }

        private string AesEncrypt(byte[] clearBytes, DeriveBytes derived)
        {
            if (clearBytes == null || clearBytes.Length == 0) throw new ArgumentException("Nothing to encrypt", "clearBytes");
            if (derived == null) throw new ArgumentException("Nothing to encrypt", "derived");
            var enc = string.Empty;
            using (var encryptor = Aes.Create())
            {
                encryptor.Key = derived.GetBytes(KeySize);
                encryptor.IV = derived.GetBytes(InitVectorSize);
                encryptor.Mode = CipherMode.CBC;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    enc = Convert.ToBase64String(ms.ToArray());
                }
            }
            return enc;
        }
    }
}
