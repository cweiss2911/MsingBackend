using FileReader.FileHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileReader.FileReader
{


    public class PoorMansFileReader : IFileReader
    {
        public bool Activated { get; set; } = true;
        public int CheckInterval { get; set; } = 1000;

        private string _inputLocation;
        private IFileHandler _fileHandler;

        public PoorMansFileReader(string inputLocation, IFileHandler fileHandler)
        {
            _inputLocation = inputLocation;
            _fileHandler = fileHandler;
        }

        public event FileReadEventHandler FileRead;

        public void Start()
        {
            while (Activated)
            {
                Thread.Sleep(CheckInterval);
                string[] allFilesInDirectory = _fileHandler.GetAllFilesInDirectory(_inputLocation);
                                    
                for (int i = 0; i < allFilesInDirectory.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(allFilesInDirectory[i]);

                    Console.WriteLine($"got file {fileInfo.Name}");
                    FileRead(new FileReadEventArgs() { FileInfo = fileInfo });
                }
            }
        }
    }
}
