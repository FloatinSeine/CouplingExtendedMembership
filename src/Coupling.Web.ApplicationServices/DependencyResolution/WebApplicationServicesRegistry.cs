
using Coupling.Web.ApplicationServices.Implementation.Cryptography;
using Coupling.Web.ApplicationServices.Implementation;
using StructureMap.Configuration.DSL;

namespace Coupling.Web.ApplicationServices.DependencyResolution
{
    public class WebApplicationServicesRegistry : Registry
    {
        public WebApplicationServicesRegistry()
        {
            For<IAccountService>().Use<AccountService>();
            For<IEncrypt>().Use<CryptographyService>();
        }
    }
}
