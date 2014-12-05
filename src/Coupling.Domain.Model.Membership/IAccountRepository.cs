
using Coupling.Domain.Persistence;

namespace Coupling.Domain.Model.Membership
{
    public interface IAccountRepository : IRepository<Account>
    {
        void Store(Account account);
        Account GetByUsername(string username);
        Account GetByConfirmationToken(string confirmationToken);
        Account GetByOAuthProvider(string provider, string providerUserId);
        Account GetByUserId(int userId);
        int GetNextUserId();
        void AppendOAuthAccount(string username, string provider, string providerUserId);
        void ChangePassword(string id, string salt, string password);
        void ActivateAccount(string accountId, string activationToken);
        bool ValidateCredentials(string accountId, string password);
        //bool AccountExists(string username, string passwordHash);
        void AppendRoles(string accountId, string[] roles);
        void RemoveRoles(string accountId, string[] roles);
    }
}
