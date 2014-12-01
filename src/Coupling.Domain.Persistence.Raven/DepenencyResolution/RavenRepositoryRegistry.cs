using Coupling.Domain.Model.Membership;
using Coupling.Domain.Persistence.Raven.Implementation;
using Raven.Client;
using StructureMap.Configuration.DSL;


namespace Coupling.Domain.Persistence.Raven.DepenencyResolution
{
    public class RavenRepositoryRegistry : Registry
    {
        public RavenRepositoryRegistry()
        {
            ForSingletonOf<IRavenSessionFactoryBuilder>().Use<CouplingRavenSessionFactoryBuilder>();

            ForSingletonOf<IRavenSessionFactory>()
                .Use("Fetch Raven Session Factory", ctx => ctx.GetInstance<IRavenSessionFactoryBuilder>().GetSessionFactory());

            For<IDocumentSession>()
                .Use("Fetch Raven IDocumentSession", ctx => ctx.GetInstance<IRavenSessionFactory>().CreateSession());

            For<IAccountRepository>().Use<AccountRepository>();

        }

    }
}
