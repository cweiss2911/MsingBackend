using FileReader.FileReader;
using FileReaderTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderTest
{
    [TestClass]
    public class PoorMansFileReaderTest
    {
        private FileHandlerMock _fileHandler;
        private PoorMansFileReader _poorMansFileReader;
        private FileReadEventArgs _fileReadEventArgs;

        public PoorMansFileReaderTest()
        {
            _fileHandler = new FileHandlerMock();
            _poorMansFileReader = new PoorMansFileReader("Input", _fileHandler);
            _poorMansFileReader.FileRead += OnPoorMansFileReaderFileRead;
            _poorMansFileReader.CheckInterval = 1;
        }

        private void OnPoorMansFileReaderFileRead(FileReadEventArgs e)
        {
            _fileReadEventArgs = e;
            _poorMansFileReader.Activated = false;

        }

        [TestMethod]
        public void ReadFile()
        {
            new Task(new Action(() =>
            {
                _poorMansFileReader.Start();
            })).Start();

            bool keepWaiting = true;
            DateTime startOfWait = DateTime.Now;
            while (keepWaiting)
            {
                if (!_poorMansFileReader.Activated)
                {
                    keepWaiting = false;
                }
                else
                {
                    var totalWaitTime = (DateTime.Now - startOfWait).TotalMilliseconds;
                    if (totalWaitTime > 250)
                    {
                        _poorMansFileReader.Activated = false;
                        Assert.Fail("No file read");
                    }
                }
            }

            Assert.IsNotNull(_fileReadEventArgs);
            Assert.IsNotNull(_fileReadEventArgs.FileInfo);
            Assert.AreEqual("FileReadIn", _fileReadEventArgs.FileInfo.Name);

        }
    }
}
