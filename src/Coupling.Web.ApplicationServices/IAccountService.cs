
using System;
using Coupling.Domain.Model.Membership;

namespace Coupling.Web.ApplicationServices
{
    public interface IAccountService
    {
        string CreateAccount(string username, string password, bool requiresConfirmation);
        bool ConfirmAccount(string confirmationToken);
        bool ConfirmAccount(string userName, string confirmationToken);

        bool ValidateAccount(string username, string password);
        DateTime GetLastPasswordFailureDate(string userName);
        DateTime GetPasswordChangedDate(string userName);
        int GetPasswordFailuresSinceLastSuccess(string userName);
        string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow);
        bool ResetPasswordWithToken(string token, string newPassword);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);

        bool DeleteAccount(string userName);
        Account GetAccount(string username, bool isOnline);
        
    }
}
