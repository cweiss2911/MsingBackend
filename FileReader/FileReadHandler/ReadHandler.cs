using Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader.FileReadHandler
{
    public class ReadHandler : IReadHandler
    {
        private INotifier _notifier;

        public ReadHandler(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void HandleReadFile(FileInfo fileInfo)
        {
            SendMessage(fileInfo.Name);

            DeleteFile(fileInfo.FullName);
        }

        private void DeleteFile(string fullName)
        {
            Console.WriteLine("Deleting");
            File.Delete(fullName);
            Console.WriteLine("Deleted");
        }


        private void SendMessage(string payload)
        {
            Message message = new Message()
            {
                Type = "success",
                Payload = payload,
            };

            _notifier.Notify(new JsonContent(message));
        }

    }
}
