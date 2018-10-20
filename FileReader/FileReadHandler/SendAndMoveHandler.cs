using FileReader.Notifier;
using Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader.FileReadHandler
{
    public class SendAndMoveHandler : IReadHandler
    {
        private INotifier _fileReadNotifier;
        private INotifier _fileContentNotifier;
        private string _processedLocation;

        public SendAndMoveHandler(INotifier fileReadNotifier, INotifier fileContentNotifier, string processedLocation)
        {
            _fileReadNotifier = fileReadNotifier;
            _fileContentNotifier = fileContentNotifier;
            _processedLocation = processedLocation;
        }

        public void HandleReadFile(FileInfo fileInfo)
        {
            SendMessages(fileInfo);
            MoveFile(fileInfo);
        }

        private void MoveFile(FileInfo fileInfo)
        {
            //string target = Path.Combine(_processedLocation, $"{DateTime.Now.Ticks}{fileInfo.Name}").ToString();
            string target = Path.Combine(_processedLocation, $"{fileInfo.Name}").ToString();
            Console.WriteLine($"moving to {target}");
            File.Move(fileInfo.FullName, target);
        }

        private void SendMessages(FileInfo fileInfo)
        {
            SendFileReadMessage(fileInfo);
            SendFileContentMessage(fileInfo);
        }

        private void SendFileReadMessage(FileInfo fileInfo)
        {
            FileReadMessage fileReadMessage = new FileReadMessage()
            {
                FileName = fileInfo.Name,
            };
            _fileReadNotifier.Notify(fileReadMessage);
        }

        private void SendFileContentMessage(FileInfo fileInfo)
        {
            string payload = File.ReadAllText(fileInfo.FullName);

            FileContentMessage message = new FileContentMessage()
            {
                FileName = fileInfo.Name,
                Payload = payload
            };

            _fileContentNotifier.Notify(message);
        }

    }
}
