using System;
using System.Collections.Generic;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Membership
{
    public class PasswordResetOptions : ValueObject
    {
        public PasswordResetOptions(string password, string token, DateTime expiry)
        {
            Password = password;
            ResetToken = token;
            ExpiryDate = expiry;
        }

        public string Password { get; private set; }
        public string ResetToken { get; private set; }
        public DateTime ExpiryDate { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Password;
            yield return ResetToken;
            yield return ExpiryDate;
        }
    }
}
