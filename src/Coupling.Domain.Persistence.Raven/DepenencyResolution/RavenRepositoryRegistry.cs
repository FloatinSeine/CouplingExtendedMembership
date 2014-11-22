using Coupling.Domain.Model.Membership;
using Coupling.Domain.Persistence.Raven.Implementation;
using Raven.Client;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Coupling.Domain.Persistence.Raven.DepenencyResolution
{
    public class RavenRepositoryRegistry : Registry
    {
        public RavenRepositoryRegistry()
        {
            For<IRavenSessionFactoryBuilder>().Use<CouplingRavenSessionFactoryBuilder>();
            //ForSingletonOf<IRavenSessionFactory>().Use<RavenSessionFactory>();

            For<IRavenSessionFactory>()
                    .UseSpecial(y => y.ConstructedBy("Fetch Raven Session Factory", z => z.GetInstance<IRavenSessionFactoryBuilder>()
                                .GetSessionFactory()));

            For<IDocumentSession>()
                .UseSpecial(
                    y =>
                        y.ConstructedBy("Fetch Raven IDocumentSession",
                            z => z.GetInstance<IRavenSessionFactory>().CreateSession()));

            For<IAccountRepository>(Lifecycles.Transient).Use<AccountRepository>();
        }

    }
}
