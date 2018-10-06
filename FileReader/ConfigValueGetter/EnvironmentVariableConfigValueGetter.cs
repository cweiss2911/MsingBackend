using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.ConfigValueGetter
{
    public class EnvironmentVariableConfigValueGetter : IConfigValueGetter
    {
        public string GetConfigValue(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
