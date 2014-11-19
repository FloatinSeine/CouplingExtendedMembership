
using Coupling.Domain.CQRS.Command;
using Coupling.Domain.Membership;
using Coupling.Domain.Membership.Implementation;
using Coupling.Domain.Membership.Implementation.Queries;
using StructureMap.Configuration.DSL;

namespace Coupling.Domain.DepenencyResolution
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            ForSingletonOf<IBus>().Use<InProcessBus>();
            ForSingletonOf<IAccountFactory>().Use<AccountFactory>();
            ForSingletonOf<IFindAccountQuery>().Use<AccountFinder>();
            ForSingletonOf<IFailedPasswordQuery>().Use<FailedPasswordQuery>();
        }

    }
}
