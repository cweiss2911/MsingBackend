namespace IncomingMessageHandler.Config
{
    public class IncomingMessageHandlerConfig
    {
        public Messaging Messaging { get; set; } = new Messaging();
    }

    public class Messaging
    {
        public string ServerAddress { get; set; }
        public string FileContentChannel { get; set; }
    }
}
