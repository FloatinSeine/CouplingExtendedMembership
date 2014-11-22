
using Raven.Client;

namespace Coupling.Domain.Persistence.Raven.Implementation
{
    public class RavenSessionFactory : IRavenSessionFactory
    {
        private readonly IDocumentStore _documentStore;

        public RavenSessionFactory(IDocumentStore documentStore)
        {
            //if (_documentStore != null) return;
            _documentStore = documentStore;
            _documentStore.Initialize();
        }

        public IDocumentSession CreateSession()
        {
            return _documentStore.OpenSession();
        }
    }
}
