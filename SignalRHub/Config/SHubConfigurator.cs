using ConfigValueGetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub.Config
{
    public class SHubConfigurator
    {
        private const string ServerAddress = "ServerAddress";
        private const string FileReadChannel = "FileReadChannel";


        public SHubConfig UpdateConfigIfRanInContainer(SHubConfig sHubConfig)
        {
            if (ContainerChecker.AmIinAContainer())
            {
                EnvironmentVariableConfigValueGetter configValueGetter = new EnvironmentVariableConfigValueGetter();
                SHubConfig containerSHubConfig = new SHubConfig();

                containerSHubConfig.Messaging.ServerAddress = configValueGetter.GetConfigValue(ServerAddress);                
                containerSHubConfig.Messaging.FileReadChannel = configValueGetter.GetConfigValue(FileReadChannel);

                return containerSHubConfig;
            }
            else
            {
                return sHubConfig;
            }
        }

    }
}
