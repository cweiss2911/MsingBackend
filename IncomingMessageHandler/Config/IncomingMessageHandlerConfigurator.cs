using ConfigValueGetter;

namespace IncomingMessageHandler.Config
{
    public class IncomingMessageHandlerConfigurator
    {
        private const string ServerAddress = "ServerAddress";
        private const string FileContentChannel = "FileContentChannel";


        public IncomingMessageHandlerConfig UpdateConfigIfRanInContainer(IncomingMessageHandlerConfig incomingMessageConfig)
        {
            if (ContainerChecker.AmIinAContainer())
            {
                EnvironmentVariableConfigValueGetter configValueGetter = new EnvironmentVariableConfigValueGetter();
                IncomingMessageHandlerConfig newIncomingMessageConfig = new IncomingMessageHandlerConfig();

                newIncomingMessageConfig.Messaging.ServerAddress = configValueGetter.GetConfigValue(ServerAddress);                
                newIncomingMessageConfig.Messaging.FileContentChannel = configValueGetter.GetConfigValue(FileContentChannel);

                return newIncomingMessageConfig;
            }
            else
            {
                return incomingMessageConfig;
            }
        }

    }
}
