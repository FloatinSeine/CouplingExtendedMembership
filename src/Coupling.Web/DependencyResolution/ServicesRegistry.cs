
using StructureMap.Configuration.DSL;

namespace Coupling.Web.DependencyResolution
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            //ForSingletonOf<IHandleSoftwareRequests>().Use<UpdateSoftwareRequestHandler>();
        }
    }
}