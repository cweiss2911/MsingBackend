using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader.FileHandling
{
    public class FileHandlerImplementation : IFileHandler
    {
        private string _processedLocation;

        public FileHandlerImplementation(string processedLocation)
        {
            _processedLocation = processedLocation;
        }

        public string[] GetAllFilesInDirectory(string inputLocation)
        {
            return Directory.GetFiles(inputLocation, "*.*", SearchOption.AllDirectories);
        }

        public void MoveFile(FileInfo fileInfo)
        {
            string target = Path.Combine(_processedLocation, $"{fileInfo.Name}").ToString();
            Console.WriteLine($"moving to {target}");
            File.Move(fileInfo.FullName, target);
        }

        public string ReadAllText(string fullName)
        {
            return File.ReadAllText(fullName);
        }
    }
}
