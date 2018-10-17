using ConfigValueGetter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileReader
{
    public class FileReaderConfigurator
    {
        private IConfigValueGetter _configValueGetter;

        private const string ContainerFlag = "ContainerFlag";
        private const string InputLocation = "InputLocation";
        private const string NotificationTarget = "NotificationTarget";
        private const string FileReadTopicName = "FileReadTopicName";
        private const string KafkaServerAddress = "KafkaServerAddress";
        
        public FileReaderConfigurator()
        {            
            if (ContainerChecker.AmIinAContainer())
            {
                _configValueGetter = new EnvironmentVariableConfigValueGetter();
            }
            else
            {
                _configValueGetter = new AppConfigValueGetter();
            }
        }

        public FileReaderConfig ReadConfig()
        {
            FileReaderConfig fileReaderConfig = new FileReaderConfig();

            fileReaderConfig.InputLocation = _configValueGetter.GetConfigValue(InputLocation);
            fileReaderConfig.NotificationTarget = _configValueGetter.GetConfigValue(NotificationTarget);
            fileReaderConfig.FileReadTopicName = _configValueGetter.GetConfigValue(FileReadTopicName);
            fileReaderConfig.KafkaServerAddress = _configValueGetter.GetConfigValue(KafkaServerAddress);

            return fileReaderConfig;
        }

    }
}
