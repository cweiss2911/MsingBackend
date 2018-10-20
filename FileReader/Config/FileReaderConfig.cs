using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
    public class FileReaderConfig
    {
        public string InputLocation { get; set; }
        public string NotificationTarget { get; set; }
        public string KafkaServerAddress { get; set; }
        public string FileReadTopicName { get; set; }
        public string ProcessedLocation { get; set; }
        public string FileContentTopicName { get; set; }
    }
}
