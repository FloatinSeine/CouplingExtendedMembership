using Coupling.Security.Implementation;
using StructureMap.Configuration.DSL;

namespace Coupling.Security.DependencyResolution
{
    public class SecurityRepository : Registry
    {
        public SecurityRepository()
        {
            For<IEncrypt>().Use<CryptographyService>();
        }
    }
}
