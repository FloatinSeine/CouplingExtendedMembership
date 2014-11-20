
using Coupling.Domain.CQRS.Command;
using StructureMap.Configuration.DSL;

namespace Coupling.Domain.DepenencyResolution
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            ForSingletonOf<IBus>().Use<InProcessBus>();
        }

    }
}
