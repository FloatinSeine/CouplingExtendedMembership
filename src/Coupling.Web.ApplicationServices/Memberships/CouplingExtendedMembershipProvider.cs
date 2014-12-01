using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using Coupling.Domain.Model.Membership;
using WebMatrix.WebData;
using Coupling.Web.ApplicationServices.Extensions;


namespace Coupling.Web.ApplicationServices.Memberships
{
    public class CouplingExtendedMembershipProvider : ExtendedMembershipProvider
    {
        //private IAccountService _service;
        private bool _isInitialised;

        private int _maxInvalidPasswordAttempts;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private int _passwordAttemptWindow;
        private bool _enablePasswordRetrieval;
        private bool _requiresUniqueEmail;
        private bool _requiresQuestionAndAnswer;
        private bool _enablePasswordReset;
        private string _passwordStregthRegularExpression;
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
            _passwordStregthRegularExpression = config.GetValue("passwordStrengthRegularExpression", PasswordStrengthRegularExpressions.StrengthWeek);

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
            //_service = DependencyResolver.Current.GetService<IAccountService>();
            _isInitialised = true; //(_service != null);
        }

        private static IAccountService AccountService
        {
            get { return DependencyResolver.Current.GetService<IAccountService>(); }
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
            get { return _passwordStregthRegularExpression; }
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


        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            VerifyInitialised();
            ValidateArgument("accountConfirmationToken", accountConfirmationToken);
            return AccountService.ConfirmAccount(accountConfirmationToken);
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);
            ValidateArgument("accountConfirmationToken", accountConfirmationToken);

            return AccountService.ConfirmAccount(userName, accountConfirmationToken);
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
            if (AccountService.GetAccount(userName, true) != null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
            }

            return AccountService.CreateAccount(userName, password, requireConfirmationToken);
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {

            var confirm = CreateAccount(userName, password, requireConfirmation);

            return confirm;
        }

        public override bool DeleteAccount(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);

            throw new NotImplementedException("Membership accounts can not be directly deleted from website.");
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);

            return AccountService.GeneratePasswordResetToken(userName, tokenExpirationInMinutesFromNow);
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);

            var acc = AccountService.GetAccount(userName, true);
            if (acc.AuthMemberships.Count == 0) return new Collection<OAuthAccountData>();
            return acc.AuthMemberships.Select(mem => new OAuthAccountData(mem.Provider, mem.ProviderUserId)).ToList();
        }

        public override DateTime GetCreateDate(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);


            var acc = AccountService.GetAccount(userName, true);
            return acc.Created;
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);
            return AccountService.GetLastPasswordFailureDate(userName);
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);
            return AccountService.GetPasswordChangedDate(userName);
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);
            return AccountService.GetPasswordFailuresSinceLastSuccess(userName);
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            VerifyInitialised();
            throw new NotImplementedException();
        }

        public override bool IsConfirmed(string userName)
        {
            VerifyInitialised();
            ValidateArgument("userName", userName);
            var acc = AccountService.GetAccount(userName, true);
            return (acc.AccountStatus == AccountStatus.Activated);
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            VerifyInitialised();
            ValidateArgument("token", token);
            ValidateArgument("newPassword", newPassword);
            return AccountService.ResetPasswordWithToken(token, newPassword);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            VerifyInitialised();
            ValidateArgument("username", username);
            ValidateArgument("oldPassword", oldPassword);
            ValidateArgument("newPassword", newPassword);

            return AccountService.ChangePassword(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            VerifyInitialised();
            ValidateArgument("username", username);
            ValidateArgument("password", password);
            ValidateArgument("newPasswordQuestion", newPasswordQuestion);
            ValidateArgument("newPasswordAnswer", newPasswordAnswer);

            return AccountService.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
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
            ValidateArgument("username", username);
            var acc = AccountService.GetAccount(username, userIsOnline);
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
            ValidateArgument("username", username);
            ValidateArgument("password", password);

            return AccountService.ValidateAccount(username, password);
        }

        public override bool HasLocalAccount(int userId)
        {
            VerifyInitialised();
            var usn = GetUserNameFromId(userId);
            var acc = AccountService.GetAccount(usn, true);
            return (acc.Membership != null);
        }

        public override string GetUserNameFromId(int userId)
        {
            VerifyInitialised();
            return AccountService.GetUserNameFromId(userId);
        }

        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            VerifyInitialised();
            return AccountService.GetUserIdFromOAuth(provider, providerUserId);
        }

        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            VerifyInitialised();
            AccountService.AppendOAuthAccount(userName, provider, providerUserId);
        }


        public override void DeleteOAuthAccount(string provider, string providerUserId)
        {
            throw new NotImplementedException();
            //base.DeleteOAuthAccount(provider, providerUserId);
        }

        private static void ValidateArgument(string argumentName, string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(string.Format("Invalid Argument {0}", argumentName), argumentName);
        }

    }
}
