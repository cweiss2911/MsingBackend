using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileReader.FileReader
{


    public class PoorMansFileReader : IFileReader
    {
        private string _inputLocation;

        public PoorMansFileReader(string inputLocation)
        {
            _inputLocation = inputLocation;
        }

        public event FileReadEventHandler FileRead;

        public void Start()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var all = Directory.GetFiles(_inputLocation, "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < all.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(all[i]);

                    Console.WriteLine($"got file {fileInfo.Name}");
                    FileRead(new FileReadEventArgs() { FileInfo = fileInfo });
                }
            }
        }
    }
}
