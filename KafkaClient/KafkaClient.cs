using Confluent.Kafka;
using Messages;
using System;
using System.Threading.Tasks;

namespace KafkaMessaging
{
    public class KafkaClient : IMessageClient
    {
        private Consumer<Ignore, string> _consumer;
        private Task _task;

        public event MessageReceivedEventHandler MessageReceived;

      

        public void Connect(string connection)
        {
            Console.WriteLine($"address is {connection}");

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = connection,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // eariest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetResetType.Earliest
            };

            _consumer = new Consumer<Ignore, string>(conf);


            _consumer.OnError += (_, error)
              => Console.WriteLine($"Error: {error}");
        }

        public void AddChannel(string channelName)
        {
            _consumer.Subscribe(channelName);
        }


        public void StartListening()
        {
            bool consuming = true;
            _task = Task.Run(new Action(() =>
            {

                while (consuming)
                {
                    try
                    {
                        var msg = _consumer.Consume();

                        Console.WriteLine($"Consumed message '{msg.Value}' at: '{msg.TopicPartitionOffset}'.");

                        MessageReceivedEventArgs messageReceivedEventArgs = new MessageReceivedEventArgs()
                        {
                            Channel = msg.Topic,
                            Message = msg.Value
                        };
                        MessageReceived(messageReceivedEventArgs);
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