using IncomingMessageHandler.Config;
using KafkaMessaging;
using Messages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace IncomingMessageHandler
{
    public class IncomingMessageMessageClient
    {
        public IncomingMessageHandlerConfig Settings { get; set; }

        private IMessageClient _messageClient;

        public IncomingMessageMessageClient(IMessageClient messageClient, IOptionsMonitor<IncomingMessageHandlerConfig> settings)
        {
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
            Console.WriteLine("Received");
            Console.WriteLine(e.Message);
            FileContentMessage fileContentMessage = JsonConvert.DeserializeObject<FileContentMessage>(e.Message);
            Console.WriteLine(fileContentMessage.Payload);
        }


        public void StartListening()
        {
            Console.WriteLine($"Listening to {Settings.Messaging.FileContentChannel}");
            _messageClient.AddChannel(Settings.Messaging.FileContentChannel);

            _messageClient.StartListening();
        }


    }
}