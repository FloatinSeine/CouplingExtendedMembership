using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coupling.Domain.Model.Membership;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Coupling.Domain.Persistence.Raven.Indexes
{
    public class IndexOAuthMemberships : AbstractIndexCreationTask<Account, OAuthMembershipsQueryResult>
    {
        public IndexOAuthMemberships()
        {
            Map = accounts => from account in accounts
                select new
                {
                    UserName = account.Username,
                    OAuthProvider = account.AuthMemberships.Select(x => x.Provider),
                    OAuthProviderUserId = account.AuthMemberships.Select(x => x.ProviderUserId)
                };

            Store(x => x.UserName, FieldStorage.Yes);

            Index(x => x.Provider, FieldIndexing.Analyzed);
            Index(x => x.ProviderUserId, FieldIndexing.Analyzed);
        }
    }

    public class OAuthMembershipsQueryResult
    {
        public string UserName { get; set; }
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
    }
}
