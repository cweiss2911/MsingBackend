using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalRHub.Config;
using System;
using System.Threading.Tasks;

namespace SignalRHub
{
    public class KafkaClient : IKafkaClient
    {
        public SHubConfig Settings { get; set; }

        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        private Consumer<Ignore, string> _consumer;
        private Task _task;

        public KafkaClient(IHubContext<NotifyHub, ITypedHubClient> hubContext, IOptionsMonitor<SHubConfig> settings)
        {
            _hubContext = hubContext;
            Settings = settings.CurrentValue;
        }


        public void StartListening()
        {
            Console.WriteLine($"address is {Settings.KafkaServerAddress}");
     
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = Settings.KafkaServerAddress,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // eariest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetResetType.Earliest
            };

            _consumer =  new Consumer<Ignore, string>(conf);

            
            _consumer.OnError += (_, error)
              => Console.WriteLine($"Error: {error}");

            
            _consumer.Subscribe(Settings.FileReadTopicName);

            bool consuming = true;
            _task = Task.Run(new Action(() =>
            {
                
                while (consuming)
                {
                    try
                    {
                        var msg = _consumer.Consume();
                        Messages.Message message = JsonConvert.DeserializeObject<Messages.Message>(msg.Value);
                        _hubContext.Clients.All.BroadcastMessage(message.Type, message.Payload);
                        Console.WriteLine($"Consumed message '{msg.Value}' at: '{msg.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }));
        }

    }
}