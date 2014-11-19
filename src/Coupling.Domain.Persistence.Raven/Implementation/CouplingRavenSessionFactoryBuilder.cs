using System;
using System.Configuration;
using Raven.Client.Document;


namespace Coupling.Domain.Persistence.Raven.Implementation
{
    public class CouplingRavenSessionFactoryBuilder : IRavenSessionFactoryBuilder
    {
        private const string ConnectionStringName = "CouplingDataStore";
        private IRavenSessionFactory _ravenSessionFactory;

        public CouplingRavenSessionFactoryBuilder()
        {
        }

        public IRavenSessionFactory GetSessionFactory()
        {
            return _ravenSessionFactory ?? (_ravenSessionFactory = CreateSessionFactory(ConnectionStringName));
        }

        private static IRavenSessionFactory CreateSessionFactory(string connectionName)
        {
            return new RavenSessionFactory(new DocumentStore
            {
                ConnectionStringName = connectionName,
            });
        }

        private string GetConnectionString()
        {
            return ConnectionStringName;

            //string s = string.Empty;
            //try
            //{
            //    s = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(this.GetType(), "Cant find ConnectionString Name: " + ConnectionStringName, ex);
            //}
            //return s;
        }
    }
}
