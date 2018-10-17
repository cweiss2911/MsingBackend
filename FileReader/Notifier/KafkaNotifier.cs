using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Notifier
{
    public class KafkaNotifier : INotifier
    {
        private Producer<string, string> _producer;
        private string _topic;

        public KafkaNotifier(string serverAddress, string topic)
        {
            _topic = topic;

            var topicConfig = new Dictionary<string, object>
            {
                { "acks", 1 }
            };

            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", serverAddress },
                { "retries", 0 },
                { "client.id", "someId" },
                { "batch.num.messages", 1 },
                { "socket.blocking.max.ms", 1 },
                { "socket.nagle.disable", true },
                { "queue.buffering.max.ms", 0 },
                { "default.topic.config", topicConfig },
            };

            Console.WriteLine($"connecting to {serverAddress}");
            _producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8));
            _producer.OnError += _producer_OnError;
            Console.WriteLine($"connected");
        }

        private void _producer_OnError(object sender, Error e)
        {
            Console.WriteLine(e);
        }

        public void Notify(Messages.Message message)
        {
            string json = JsonConvert.SerializeObject(message);

            Console.WriteLine($"sending message to topic {_topic}");
            var dr = _producer.ProduceAsync(_topic, null, json).Result;
            Console.WriteLine("message sent");
        }
    }
}
