using Confluent.Kafka;
using KafkaMessaging;
using Messages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalRHub.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub
{
    public class HubMessageClient 
    {
        public SHubConfig Settings { get; set; }

        private IMessageClient _messageClient;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        
        public HubMessageClient(IHubContext<NotifyHub, ITypedHubClient> hubContext, IMessageClient messageClient, IOptionsMonitor<SHubConfig> settings)
        {
            _hubContext = hubContext;
            Settings = settings.CurrentValue;
            _messageClient = messageClient;

            _messageClient.MessageReceived += OnMessageReceived;
        }

        public void Connect()
        {
            _messageClient.Connect(Settings.Messaging.ServerAddress);
        }

        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            FileReadMessage message = JsonConvert.DeserializeObject<FileReadMessage>(e.Message);
            _hubContext.Clients.All.BroadcastMessage(message.Type, message.FileName);
        }


        public void StartListening()
        {
            _messageClient.AddChannel(Settings.Messaging.FileReadChannel);

            _messageClient.StartListening();
        }

        
    }
}