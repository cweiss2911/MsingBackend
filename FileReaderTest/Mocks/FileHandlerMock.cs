using FileReader.FileHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReaderTest.Mocks
{
    public class FileHandlerMock : IFileHandler
    {
        public bool FileMoved { get; set; } = false;
        public string ReadText { get; set; }

        public string[] GetAllFilesInDirectory(string inputLocation)
        {
            return new string[] { "FileReadIn" };
        }

        public void MoveFile(FileInfo fileInfo)
        {
            FileMoved = true;
        }

        public string ReadAllText(string fullName)
        {
            return ReadText;    
        }
    }
}
