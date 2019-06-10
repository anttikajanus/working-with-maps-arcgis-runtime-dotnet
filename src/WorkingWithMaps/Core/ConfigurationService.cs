using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Example.Core
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(key);

            var setting = ConfigurationManager.AppSettings[key];
            return setting;
        }
    }
}
