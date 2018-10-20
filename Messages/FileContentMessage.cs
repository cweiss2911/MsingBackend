using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public class FileContentMessage : FileReadMessage
    {        
        public string Payload { get; set; }

        public FileContentMessage()
        {
            Type = nameof(FileContentMessage);
        }

    }
}
