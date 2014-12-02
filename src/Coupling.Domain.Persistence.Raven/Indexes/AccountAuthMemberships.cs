using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Coupling.Domain.Model.Membership;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Coupling.Domain.Persistence.Raven.Indexes
{
    public class AccountAuthMemberships : AbstractIndexCreationTask<Account, AccountAuthMemberships.AuthResults>
    {
        public class AuthResults
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Provider { get; set; }
            public string ProviderUserId { get; set; }
            public string AccountId { get; set; }
        }

        public AccountAuthMemberships()
        {
            Map = accounts => from account in accounts
                              from auth in account.AuthMemberships
                                select new
                                {
                                    AccountId = account.Id,
                                    account.Username,
                                    auth.Provider,
                                    auth.ProviderUserId,
                                };


            Store(x => x.Username, FieldStorage.Yes);
            Store(x => x.AccountId, FieldStorage.Yes);

            Index(x => x.Provider, FieldIndexing.Analyzed);
            Index(x => x.ProviderUserId, FieldIndexing.Analyzed);
        }

        public override IndexDefinition CreateIndexDefinition()
        {
            var definintion = base.CreateIndexDefinition();
            definintion.SortOptions.Add("Provider", SortOptions.String);
            definintion.SortOptions.Add("ProviderUserId", SortOptions.Int);

            return definintion;
        }
    }
}
