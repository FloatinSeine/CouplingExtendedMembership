using Raven.Client;

namespace Coupling.Domain.Persistence.Raven
{
    public interface IRavenSessionFactory
    {
        IDocumentSession CreateSession();
    }
}
