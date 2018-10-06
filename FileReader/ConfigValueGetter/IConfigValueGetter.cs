using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.ConfigValueGetter
{
    public interface IConfigValueGetter
    {
        string GetConfigValue(string key);
    }
}
