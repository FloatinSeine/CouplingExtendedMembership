using System;
using System.Linq;
using System.Linq.Expressions;
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
            using (var session = _factory.CreateSession())
            {
                session.Store(account);
                session.SaveChanges();
            }
        }

        public Account GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            Account acc;
            using (var session = _factory.CreateSession())
            {
                acc = session.Query<Account>().SingleOrDefault(x => x.Username == username);
            }
            return acc;
        }

        public Account GetByConfirmationToken(string activationToken)
        {
            Account acc;
            using (var session = _factory.CreateSession())
            {
                acc = session.Query<Account>().SingleOrDefault(x => x.ActivationToken == activationToken);
            }
            return acc;
        }

        public Account GetByOAuthProvider(string provider, string providerUserId)
        {
            if (string.IsNullOrEmpty(provider)) throw new ArgumentException(string.Format("Invalid Argument {0}", provider), "provider");
            if (string.IsNullOrEmpty(providerUserId)) throw new ArgumentException(string.Format("Invalid Argument {0}", providerUserId), "providerUserId");

            Account acc = null;
            using (var session = _factory.CreateSession())
            {
                try
                {
                    var query = session.Query<Account, AccountAuthMemberships>()
                        .ProjectFromIndexFieldsInto<AccountAuthMemberships.AuthResults>()
                        .Where(x => x.Provider == provider && x.ProviderUserId == providerUserId)
                        .SingleOrDefault();

                    if (query != null) acc = session.Query<Account>().Single(x => x.Username == query.Username);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Cant find account for Provider {0}, UserId {1}", provider, providerUserId), ex);
                } 
            }
            return acc;
        }

        public Account GetByUserId(int userId)
        {
            Account acc;
            using (var session = _factory.CreateSession())
            {
                acc = session.Query<Account>().SingleOrDefault(x => x.UserId == userId);
            }
            return acc;
        }

        public int GetNextUserId()
        {
            using (var session = _factory.CreateSession())
            {
                var max = session.Query<Account>().Max(x => x.UserId);
                return (max > 0) ? max + 1 : 1;
            }
        }

        public void AppendOAuthAccount(string accountId, string provider, string providerUserId)
        {
            using (var session = _factory.CreateSession())
            {
                var acc = session.Load<Account>(accountId);
                acc.AppendOAuthMembership(new OAuthMembership(provider, providerUserId));
                session.SaveChanges();
            }
        }

        public void ActivateAccount(string accountId, string activationToken)
        {
            using (var session = _factory.CreateSession())
            {
                var acc = session.Load<Account>(accountId);
                acc.Activate(activationToken);
                session.SaveChanges();
            }
        }

        public void ChangePassword(string id, string salt, string password)
        {
            using (var session = _factory.CreateSession())
            {
                var acc = session.Load<Account>(id);
                acc.ChangePassword(salt, password);
                session.SaveChanges();
            }
        }

        public bool ValidateCredentials(string username, string password)
        {
            var b = false;
            using (var session = _factory.CreateSession())
            {
                var acc = session.Query<Account>().Single(x => x.Username == username);

                b = acc != null && acc.IsValidPassword(password);
                session.SaveChanges();
            }
            return b;
        }

        public bool AccountExists(string username, string passwordHash)
        {
            var q = Session.Query<Account>().Where(x => x.Username == username);
            return Enumerable.Any(q, m => m.IsValidPassword(passwordHash));
        }

    }
}
