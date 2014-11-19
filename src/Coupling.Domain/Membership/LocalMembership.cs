using System;
using System.Collections.Generic;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Membership
{
    public class LocalMembership : ValueObject
    {
        public LocalMembership(string salt, string password)
        {
            Password = password;
            Salt = salt;
            PasswordChangeDate = DateTime.UtcNow;
            ResetPasswordMatches();
        }

        public string Password { get; private set; }
        public string Salt { get; private set; }
        public int FailedPasswordMatchAttempts { get; private set; }
        public DateTime LastPasswordFailureDate { get; private set; }
        public DateTime PasswordChangeDate { get; private set; }
        

        public bool IsValidPassword(string passwordHash)
        {
            var b = (Password == passwordHash);
            if (b) ResetPasswordMatches();
            else FailedPasswordMatch();
            return b;
        }

        public bool ResetPassword(string salt, string passwordHash)
        {
            Salt = salt;
            Password = passwordHash;
            PasswordChangeDate = DateTime.UtcNow;
            ResetPasswordMatches();
            return true;
        }

        internal void FailedPasswordMatch()
        {
            FailedPasswordMatchAttempts = FailedPasswordMatchAttempts + 1;
            LastPasswordFailureDate = DateTime.UtcNow;
        }

        private void ResetPasswordMatches()
        {
            FailedPasswordMatchAttempts = 0;
            LastPasswordFailureDate = DateTime.MaxValue;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Password;
            yield return Salt;
            yield return PasswordChangeDate;
        }
    }
}
