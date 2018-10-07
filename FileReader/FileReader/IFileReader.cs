using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader.FileReader
{
    public class FileReadEventArgs : EventArgs
    {
        public FileInfo FileInfo { get; set; }
    }
    public delegate void FileReadEventHandler(FileReadEventArgs e);

    public interface IFileReader
    {
        void Start();
        event FileReadEventHandler FileRead;
    }
}
