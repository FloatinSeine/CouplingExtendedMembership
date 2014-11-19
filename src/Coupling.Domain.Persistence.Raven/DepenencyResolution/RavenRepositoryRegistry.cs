﻿using Coupling.Domain.Membership;
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
            ForSingletonOf<IRavenSessionFactory>().Use<RavenSessionFactory>();

            ForSingletonOf<IRavenSessionFactory>()
                    .UseSpecial(y => y.ConstructedBy("Fetch Raven Session Factory", z => z.GetInstance<IRavenSessionFactoryBuilder>()
                                .GetSessionFactory()));

            ForSingletonOf<IDocumentSession>()
                .UseSpecial(
                    y =>
                        y.ConstructedBy("Fetch Raven IDocumentSession",
                            z => z.GetInstance<IRavenSessionFactory>().CreateSession()));

            For<IAccountRepository>().Use<AccountRepository>();
        }

    }
}