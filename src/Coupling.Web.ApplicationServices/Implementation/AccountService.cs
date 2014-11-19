
using System;
using System.Security.Policy;
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Membership;
using Coupling.Domain.Membership.Commands;
using Coupling.Web.ApplicationServices.Memberships;

namespace Coupling.Web.ApplicationServices.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IEncrypt _encryptor;
        private readonly IBus _bus;
        private readonly IAccountFactory _factory;
        private readonly IFindAccountQuery _query;
        private readonly IFailedPasswordQuery _failedQuery;

        public AccountService(IEncrypt encryptor, IBus bus, IAccountFactory factory, IFindAccountQuery query, IFailedPasswordQuery failedQuery)
        {
            _encryptor = encryptor;
            _bus = bus;
            _factory = factory;
            _query = query;
            _failedQuery = failedQuery;
        }

        public string CreateAccount(string username, string password, bool requiresConfirmation)
        {
            var hash = _encryptor.Encrypt(password);
            var token = requiresConfirmation ? TokenFactory.Create() : string.Empty;

            _factory.Create(username, GetHashSalt(hash), GetHashPassword(hash), token);
            return token;
        }

        public bool ConfirmAccount(string confirmationToken)
        {
            var acc = _query.FindByConfirmationToken(confirmationToken);
            if (acc == null) throw new Exception("User Account can not be found");
            _bus.Send(new ActivateAccountCommand(acc.Id, confirmationToken));
            return true;
        }

        public bool ConfirmAccount(string userName, string confirmationToken)
        {
            var acc = GetAccount(userName, true);
            if (acc == null) throw new Exception("User Account can not be found");
            _bus.Send(new ActivateAccountCommand(acc.Id, confirmationToken));
            return true;
        }

        public bool ValidateAccount(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("Username is invalid", "username");
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("Password is invalid", "password");

            var acc = _query.FindByUserName(username);
            if (acc == null) throw new Exception("Can not find account for username " + username);

            var hash = _encryptor.Encrypt(password, acc.Membership.Salt);
            if (acc.IsValidPassword(hash)) return true;
            _bus.Send(new FailedPasswordMatch(acc.Id));
            return false;
            //return _query.IsValidCredentials(username, password);
        }

        public DateTime GetLastPasswordFailureDate(string userName)
        {
            var dto = _failedQuery.FailedPasswordDetails(userName);
            return dto.LastFailureDate;
        }

        public DateTime GetPasswordChangedDate(string userName)
        {
            var dto = _failedQuery.FailedPasswordDetails(userName);
            return dto.ChangedDate;
        }

        public int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            var dto = _failedQuery.FailedPasswordDetails(userName);
            return dto.FailedMatchAttempts;
        }

        public string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotImplementedException();
        }

        public bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }


        public bool DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(string username, bool isOnline)
        {
            return _query.FindByUserName(username);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var acc = GetAccount(username, true);
            var hash = _encryptor.Encrypt(oldPassword, acc.Membership.Salt);
            if (!acc.IsValidPassword(hash))
            {
                _bus.Send(new FailedPasswordMatch(acc.Id));
                throw new Exception("User Credentials do not match");
            }

            try
            {
                var newhash = _encryptor.Encrypt(newPassword);
                _bus.Send(new ChangePasswordCommand(acc.Id, GetHashSalt(newhash), GetHashPassword(newhash)));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send update command.", ex);
            }
            return true;
        }

        private static string GetHashSalt(string hash)
        {
            var splitIdx = hash.IndexOf("#-#", StringComparison.InvariantCulture);
            return hash.Substring(0, splitIdx);
        }
        private static string GetHashPassword(string hash)
        {
            var splitIdx = hash.IndexOf("#-#", StringComparison.InvariantCulture);
            return hash.Substring(splitIdx + 3);
        }

    }
}
