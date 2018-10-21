using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader.FileHandling
{
    public interface IFileHandler
    {
        void MoveFile(FileInfo fileInfo);
        string ReadAllText(string fullName);
        string[] GetAllFilesInDirectory(string inputLocation);
    }
}
