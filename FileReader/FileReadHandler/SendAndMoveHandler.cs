using FileReader.FileHandling;
using FileReader.Notifier;
using Messages;
using System.IO;

namespace FileReader.FileReadHandler
{
    public class SendAndMoveHandler : IReadHandler
    {
        private INotifier _fileReadNotifier;
        private INotifier _fileContentNotifier;
        private IFileHandler _fileHandler;

        public SendAndMoveHandler(INotifier fileReadNotifier, INotifier fileContentNotifier, IFileHandler fileHandler)
        {
            _fileReadNotifier = fileReadNotifier;
            _fileContentNotifier = fileContentNotifier;
            _fileHandler = fileHandler;
        }

        public void HandleReadFile(FileInfo fileInfo)
        {
            SendMessages(fileInfo);
            _fileHandler.MoveFile(fileInfo);
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
            string payload = _fileHandler.ReadAllText(fileInfo.FullName);

            FileContentMessage message = new FileContentMessage()
            {
                FileName = fileInfo.Name,
                Payload = payload
            };

            _fileContentNotifier.Notify(message);
        }

    }
}
