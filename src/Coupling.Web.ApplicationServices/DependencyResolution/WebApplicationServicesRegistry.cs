
using Coupling.Web.ApplicationServices.Implementation;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Coupling.Web.ApplicationServices.DependencyResolution
{
    public class WebApplicationServicesRegistry : Registry
    {
        public WebApplicationServicesRegistry()
        {
            For<IAccountService>(new UniquePerRequestLifecycle()).Use<AccountService>();
        }
    }
}
