using System;
using System.Collections.Generic;
using Coupling.Domain.DDD;

namespace Coupling.Domain.Membership
{
    public class OAuthMembership : ValueObject
    {

        public OAuthMembership(string provider, string providerUserId)
        {
            Provider = provider;
            ProviderUserId = providerUserId;
        }

        public string Provider { get; private set; }
        public string ProviderUserId { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Provider;
            yield return ProviderUserId;
        }
    }
}
