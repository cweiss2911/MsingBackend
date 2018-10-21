using FileReader.FileReadHandler;
using FileReader.Notifier;
using FileReaderTest.Mocks;
using Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace FileReaderTest
{
    [TestClass]
    public class SendAndMoveHandlerTest
    {
        private SendAndMoveHandler _sendAndMoveHandler;
        private NotifierMock _fileReadNotifier;
        private NotifierMock _fileContentNotifier;
        private FileHandlerMock _fileHandler;

        public SendAndMoveHandlerTest()
        {
            _fileReadNotifier = new NotifierMock();
            _fileContentNotifier = new NotifierMock();
            _fileHandler = new FileHandlerMock();
            _sendAndMoveHandler = new SendAndMoveHandler(_fileReadNotifier, _fileContentNotifier, _fileHandler);        
        }


        [TestMethod]
        public void HandleReadFile()
        {
            const string fileName = "FileName";
            const string fileContent = "TheContent";

            _fileHandler.ReadText = fileContent;

            FileInfo fileInfo = new FileInfo(fileName);
            _sendAndMoveHandler.HandleReadFile(fileInfo);

            Assert.IsTrue(_fileHandler.FileMoved);

            Message fileReadMessage = _fileReadNotifier.SentMessages.FirstOrDefault();
            Assert.IsNotNull(fileReadMessage);
            Assert.IsInstanceOfType(fileReadMessage, typeof(FileReadMessage));
            Assert.AreEqual(fileReadMessage.Type, nameof(FileReadMessage));

            FileReadMessage fileReadMessageCasted = (FileReadMessage)fileReadMessage;
            Assert.AreEqual(fileReadMessageCasted.FileName, fileName);

            Message fileContentMessage = _fileContentNotifier.SentMessages.FirstOrDefault();
            Assert.IsNotNull(fileContentMessage);
            Assert.IsInstanceOfType(fileContentMessage, typeof(FileContentMessage));
            Assert.AreEqual(fileContentMessage.Type, nameof(FileContentMessage));

            FileContentMessage fileContentMessageCasted = (FileContentMessage)fileContentMessage;
            Assert.AreEqual(fileContentMessageCasted.FileName, fileName);
            Assert.AreEqual(fileContentMessageCasted.Payload, fileContent);
        }
    }
}
