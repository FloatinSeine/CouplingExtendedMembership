using System;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Model.Membership
{
    public class LocalMembership : Entity
    {
        public LocalMembership(string salt, string password)
        {
            Password = password;
            Salt = salt;
            PasswordChangeDate = DateTime.UtcNow;
            FailedPasswordMatchAttempts = 0;
            ResetPasswordMatches();
        }

        private string Password { get; set; }
        public string Salt { get; private set; }
        public DateTime PasswordChangeDate { get; private set; }
        public int FailedPasswordMatchAttempts { get; private set; }
        public DateTime LastPasswordFailureDate { get; private set; }    

        public bool IsValidPassword(string passwordHash)
        {
            var b = (Password == passwordHash);
            if (b) ResetPasswordMatches();
            else FailedPasswordMatch();
            return b;
        }

        internal void FailedPasswordMatch()
        {
            FailedPasswordMatchAttempts = FailedPasswordMatchAttempts + 1;
            LastPasswordFailureDate = DateTime.UtcNow;
        }

        internal void ResetPasswordMatches()
        {
            FailedPasswordMatchAttempts = 0;
            LastPasswordFailureDate = DateTime.MaxValue;
        }

    }
}
