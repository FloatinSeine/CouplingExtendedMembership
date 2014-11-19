
using Raven.Client;

namespace Coupling.Domain.Persistence.Raven.Implementation
{
    public class RavenSessionFactory : IRavenSessionFactory
    {
        private readonly IDocumentStore _documentStore;

        public RavenSessionFactory(IDocumentStore documentStore)
        {
            if (_documentStore == null)
            {
                _documentStore = documentStore;

                //_documentStore.Conventions.JsonContractResolver = new IncludeNonPublicMembersContractResolver();
                //_documentStore.Conventions.CustomizeJsonSerializer = new Action<JsonSerializer>(CustomSerialisers);

                _documentStore.Initialize();
            }
        }

        //public void CustomSerialisers(JsonSerializer serialiser)
        //{
            
            //serialiser.Converters.Add(new LocalMembershipConvertor());
        //}

        public IDocumentSession CreateSession()
        {
            return _documentStore.OpenSession();
        }
    }
}
