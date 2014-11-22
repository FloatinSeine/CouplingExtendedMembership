
using System.Linq;
using Coupling.Domain.Model.Membership;
using Raven.Client.Linq;

namespace Coupling.Domain.Persistence.Raven
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {

        public AccountRepository(IRavenSessionFactory factory)
            : base(factory)
        {
        }

        public void Store(Account account)
        {
            Add(account);
            CommitChanges();
        }

        public Account GetByUsername(string username)
        {
            var acc= Session.Query<Account>().SingleOrDefault(x => x.Username == username);
            return acc;
        }

        public Account GetByConfirmationToken(string activationToken)
        {
            return Session.Query<Account>().Single(x => x.ActivationToken == activationToken);
        }

        public bool AccountExists(string username, string passwordHash)
        {
            var q = Session.Query<Account>().Where(x => x.Username == username);
            return Enumerable.Any(q, m => m.IsValidPassword(passwordHash));
        }

    }
}
