using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub.Config
{
    public class SHubConfig
    {
        public Messaging Messaging { get; set; } = new Messaging();
    }

    public class Messaging
    {
        public string ServerAddress { get; set; }
        public string FileReadChannel { get; set; }
    }
}
