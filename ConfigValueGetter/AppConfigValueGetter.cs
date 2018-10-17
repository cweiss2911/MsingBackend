using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ConfigValueGetter
{
    public class AppConfigValueGetter : IConfigValueGetter
    {
        public string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
