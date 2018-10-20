using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaMessaging
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Channel { get; set; }
        public string Message { get; set; }
    }

    public delegate void MessageReceivedEventHandler(MessageReceivedEventArgs e);


    public interface IMessageClient
    {
        void Connect(string connection);

        void StartListening();

        void AddChannel(string channelName);

        event MessageReceivedEventHandler MessageReceived;
    }
}
