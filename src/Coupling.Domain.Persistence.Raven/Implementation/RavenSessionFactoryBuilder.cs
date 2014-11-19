using System.Configuration;
using System.Web.Configuration;
using System.Web.Security;
using Raven.Client.Document;

namespace Coupling.Domain.Persistence.Raven.Implementation
{
    public class RavenSessionFactoryBuilder : IRavenSessionFactoryBuilder
    {
        private IRavenSessionFactory _ravenSessionFactory;

        public IRavenSessionFactory GetSessionFactory()
        {
            return _ravenSessionFactory ?? (_ravenSessionFactory = CreateSessionFactory(GetDefaultMembershipProviderConnectionStringName()));
        }

        private static IRavenSessionFactory CreateSessionFactory(string connectionName)
        {
            return new RavenSessionFactory(new DocumentStore
            {
                ConnectionStringName = connectionName
            });
        }

        private static string GetDefaultMembershipProviderConnectionStringName()
        {
            
            //System.Web.Security.Membership.Providers[0].

            var provider = GetDefaultMembershipProvider();
            return provider.Parameters.Get("connectionStringName");
        }

        private static ProviderSettings GetDefaultMembershipProvider()
        {
            var membership = ConfigurationManager.GetSection("system.web/membership") as MembershipSection;
            if (membership == null) throw new ConfigurationErrorsException("Error access the memberships section");

            return membership.Providers[membership.DefaultProvider];
        }
    }
}
