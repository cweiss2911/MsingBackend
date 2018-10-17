using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub.Config
{
    public class SHubConfig
    {
        public string KafkaServerAddress { get; set; }
        public string FileReadTopicName { get; set; }        
    }
}
