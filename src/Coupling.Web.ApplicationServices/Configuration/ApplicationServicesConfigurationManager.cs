using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Coupling.Web.ApplicationServices.Configuration
{
    public class ApplicationServicesConfigurationManager
    {
        private IDictionary<string, string> _appSettings;

        public IDictionary<string, string> AppSettings
        {
            get
            {
                if (_appSettings == null)
                {
                    _appSettings = ConfigurationManager.AppSettings.Cast<string>().ToDictionary(s => s, s => ConfigurationManager.AppSettings[s]);
                }
                return _appSettings;
            }
        }
    }
}
