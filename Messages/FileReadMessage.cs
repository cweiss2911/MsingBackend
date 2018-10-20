using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class FileReadMessage :Message
    {
        public string FileName { get; set; }

        public FileReadMessage()
        {
            Type = nameof(FileReadMessage);
        }
    }
}
