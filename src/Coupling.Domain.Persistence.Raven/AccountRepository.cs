
using System;
using System.Linq;
using Coupling.Domain.Model.Membership;
using Coupling.Domain.Persistence.Raven.Indexes;
using Raven.Client;
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
            if (string.IsNullOrEmpty(username)) return null;
            return Session.Query<Account>().SingleOrDefault(x => x.Username == username);
        }

        public Account GetByConfirmationToken(string activationToken)
        {
            return Session.Query<Account>().Single(x => x.ActivationToken == activationToken);
        }

        public Account GetByOAuthProvider(string provider, string providerUserId)
        {

            var query = Session.Query<OAuthMembershipsQueryResult, IndexOAuthMemberships>()
                            .Where(x => x.Provider == provider)
                            .Where(x => x.ProviderUserId == providerUserId)
                            .As<Account>()
                            .SingleOrDefault();

            if (query == null) return null;
            return query; 

        }

        public Account GetByUserId(int userId)
        {
            return Session.Query<Account>().Where(x => x.UserId == userId).SingleOrDefault();
        }

        public int GetNextUserId()
        {
            var max = Session.Query<Account>().Max(x => x.UserId);
            return (max > 0) ? max + 1 : 1;
        }

        public bool AccountExists(string username, string passwordHash)
        {
            var q = Session.Query<Account>().Where(x => x.Username == username);
            return Enumerable.Any(q, m => m.IsValidPassword(passwordHash));
        }

    }
}
