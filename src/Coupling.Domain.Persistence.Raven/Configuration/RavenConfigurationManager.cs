using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Coupling.Domain.Persistence.Raven.Configuration
{
    public class RavenConfigurationManager
    {
        public string ConnectionName
        {
            get
            {
                //ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[name];
                return "CouplingDataStore";
            }
        }

    }
}
