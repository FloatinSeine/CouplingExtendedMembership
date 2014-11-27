
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
        //bool AccountExists(string username, string passwordHash);
    }
}
