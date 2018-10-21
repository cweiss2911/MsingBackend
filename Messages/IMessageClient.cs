using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages
{
    public delegate void MessageReceivedEventHandler(MessageReceivedEventArgs e);

    public interface IMessageClient
    {
        void Connect(string connection);

        void StartListening();

        void AddChannel(string channelName);

        event MessageReceivedEventHandler MessageReceived;
    }
}
