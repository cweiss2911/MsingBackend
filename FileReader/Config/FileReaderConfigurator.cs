using FileReader.ConfigValueGetter;
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

        public FileReaderConfigurator()
        {
            if (IsInContainer())
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

            return fileReaderConfig;
        }

        private bool IsInContainer()
        {
            string dockerFlag = Environment.GetEnvironmentVariable(ContainerFlag);
            bool isInContainer = !string.IsNullOrEmpty(dockerFlag);
            return isInContainer;
        }
    }
}
