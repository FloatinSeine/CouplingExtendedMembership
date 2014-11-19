

using Coupling.Domain.Persistence;

namespace Coupling.Domain.Membership
{
    public interface IAccountRepository : IRepository<Account>
    {
        void Store(Account account);
        Account GetByUsername(string username);
        Account GetByConfirmationToken(string confirmationToken);
        bool AccountExists(string username, string passwordHash);
    }
}
