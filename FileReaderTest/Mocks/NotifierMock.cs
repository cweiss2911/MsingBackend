using FileReader.Notifier;
using Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaderTest.Mocks
{
    class NotifierMock : INotifier
    {
        public List<Message> SentMessages { get; set; } = new List<Message>();

        public void Notify(Message message)
        {
            SentMessages.Add(message);
        }
    }
}
