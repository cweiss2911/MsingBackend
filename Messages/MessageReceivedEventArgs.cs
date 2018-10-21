using System;

namespace Messages
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Channel { get; set; }
        public string Message { get; set; }
    }
}
