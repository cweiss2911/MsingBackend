using ConfigValueGetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub.Config
{
    public class SHubConfigurator
    {
        private const string KafkaServerAddress = "KafkaServerAddress";
        private const string FileReadTopicName = "FileReadTopicName";
        

        public SHubConfig UpdateConfigIfRanInContainer(SHubConfig sHubConfig)
        {
            if (ContainerChecker.AmIinAContainer())
            {
                EnvironmentVariableConfigValueGetter  configValueGetter = new EnvironmentVariableConfigValueGetter();
                SHubConfig containerSHubConfig = new SHubConfig();
                containerSHubConfig.KafkaServerAddress = configValueGetter.GetConfigValue(KafkaServerAddress);
                containerSHubConfig.FileReadTopicName = configValueGetter.GetConfigValue(FileReadTopicName);

                return containerSHubConfig;
            }
            else
            {
                return sHubConfig;
            }
        }

    }
}
