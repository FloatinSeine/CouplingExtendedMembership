using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using Coupling.Web.ApplicationServices.Extensions;


namespace Coupling.Web.ApplicationServices.Memberships
{
    public class CouplingExtendedMembershipProvider : ExtendedMembershipProvider
    {
        private IAccountService _service;
        private bool _isInitialised;

        private int _maxInvalidPasswordAttempts;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private int _passwordAttemptWindow;
        private bool _enablePasswordRetrieval;
        private bool _requiresUniqueEmail;
        private bool _requiresQuestionAndAnswer;
        private bool _enablePasswordReset;
        private MembershipPasswordFormat _passwordFormat;

        public CouplingExtendedMembershipProvider()
        {
            InitialiseService();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null) throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(name))
            {
                name = "CouplingExtendedMembershipProvider";
            }
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Coupling ExtendedMembership Provider");
            }
            base.Initialize(name, config);

            ApplicationName = config.GetValue("applicationName", HostingEnvironment.ApplicationVirtualPath);
            _maxInvalidPasswordAttempts = config.GetValue("maxInvalidPasswordAttempts", 3);
            _minRequiredNonAlphanumericCharacters = config.GetValue("minRequiredAlphaNumericCharacters", 1);
            _minRequiredPasswordLength = config.GetValue("minRequiredPasswordLength", 6);
            _passwordAttemptWindow = config.GetValue("passwordAttemptWindow", 3);
            _enablePasswordReset = config.GetValue("enablePasswordReset", true);
            _enablePasswordRetrieval = config.GetValue("enablePasswordRetrieval", false);
            _requiresQuestionAndAnswer = config.GetValue("requiresQuestionAndAnswer", false);
            _requiresUniqueEmail = config.GetValue("requiresUniqueEmail", true);
            //_passwordStregthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            
            

            SetPasswordFormat(config.GetValue("passwordFormat", "Hashed"));
        }

        private void SetPasswordFormat(string format)
        {
            switch (format)
            {
                case "Hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }
        }

        private void InitialiseService()
        {
            _service = DependencyResolver.Current.GetService<IAccountService>();
            _isInitialised = (_service != null);
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        private void VerifyInitialised()
        {
            if(!_isInitialised) throw new Exception("Account service is not initialised");
        }

        private static void ValidateUsername(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("Invalid UserName", "userName");
        }

        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            VerifyInitialised();
            if(string.IsNullOrEmpty(accountConfirmationToken)) throw new ArgumentException("Invalid Confirmation Token", accountConfirmationToken);
            return _service.ConfirmAccount(accountConfirmationToken);
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            VerifyInitialised();
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("Invalid username", "userName");
            if (string.IsNullOrEmpty(accountConfirmationToken)) throw new ArgumentException("Invalid Confirmation Token", accountConfirmationToken);
            return _service.ConfirmAccount(userName, accountConfirmationToken);
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            VerifyInitialised();
            if (string.IsNullOrEmpty(password))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidPassword);
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.InvalidUserName);
            }
            if (_service.GetAccount(userName, true) != null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
            }

            return _service.CreateAccount(userName, password, requireConfirmationToken);
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            VerifyInitialised();
           var confirm = CreateAccount(userName, password, requireConfirmation);

            return confirm;
        }

        public override bool DeleteAccount(string userName)
        {
            VerifyInitialised();
            ValidateUsername(userName);
            return _service.DeleteAccount(userName);
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            VerifyInitialised();
            ValidateUsername(userName);
            return _service.GeneratePasswordResetToken(userName, tokenExpirationInMinutesFromNow);
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            VerifyInitialised();
            var acc = _service.GetAccount(userName, true);
            if (acc.AuthMemberships.Count == 0) return new Collection<OAuthAccountData>();
            return acc.AuthMemberships.Select(mem => new OAuthAccountData(mem.Provider, mem.ProviderUserId)).ToList();
        }

        public override DateTime GetCreateDate(string userName)
        {
            VerifyInitialised();
            ValidateUsername(userName);

            var acc = _service.GetAccount(userName, true);
            return acc.Created;
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            VerifyInitialised();
            ValidateUsername(userName);
            return _service.GetLastPasswordFailureDate(userName);
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            VerifyInitialised();
            ValidateUsername(userName);
            return _service.GetPasswordChangedDate(userName);
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            VerifyInitialised();
            ValidateUsername(userName);
            return _service.GetPasswordFailuresSinceLastSuccess(userName);
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override bool IsConfirmed(string userName)
        {
            VerifyInitialised();
            ValidateUsername(userName);
            var acc = _service.GetAccount(userName, true);
            return acc.IsActivated;
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            VerifyInitialised();
            if(string.IsNullOrEmpty(token)) throw new ArgumentException("Invalid Token", "token");
            if (string.IsNullOrEmpty(newPassword)) throw new ArgumentException("Invalid Password", "newPassword");
            return _service.ResetPasswordWithToken(token, newPassword);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            VerifyInitialised();
            ValidateUsername(username);
            if (string.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Invalid Old Password");
            if (string.IsNullOrEmpty(newPassword)) throw new ArgumentException("Invalid New Password");

            return _service.ChangePassword(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            VerifyInitialised();
            ValidateUsername(username);
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Invalid password");
            if (string.IsNullOrEmpty(newPasswordQuestion)) throw new ArgumentException("Invalid question", "newPasswordQuestion");
            if (string.IsNullOrEmpty(newPasswordAnswer)) throw new ArgumentException("Invalid answer", "newPasswordAnswer");
            
            return _service.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            return Membership.GetNumberOfUsersOnline();
        }

        public override string GetPassword(string username, string answer)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            VerifyInitialised();
            ValidateUsername(username);
            var acc = _service.GetAccount(username, userIsOnline);
            return MembershipDecorator.Decorate(acc);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            VerifyInitialised();
            ValidateUsername(username);
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Invalid Password", password);

            return _service.ValidateAccount(username, password);
        }

        public override bool HasLocalAccount(int userId)
        {
            VerifyInitialised();
            return true;
        }

    }
}
