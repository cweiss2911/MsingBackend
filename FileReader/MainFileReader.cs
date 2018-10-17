using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using FileReader.FileReader;
using FileReader.FileReadHandler;
using FileReader.Notifier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileReader
{
    public class MainFileReader
    {
        private static INotifier _notifier;

        static void Main(string[] args)
        {
            FileReaderConfigurator fileReaderConfigurator = new FileReaderConfigurator();
            FileReaderConfig fileReaderConfig = fileReaderConfigurator.ReadConfig();

            //_notifier = new HttpNotifier(fileReaderConfig.NotificationTarget);
            _notifier = new KafkaNotifier(fileReaderConfig.KafkaServerAddress, fileReaderConfig.FileReadTopicName);

            IFileReader fileReader = new PoorMansFileReader(fileReaderConfig.InputLocation);
            IReadHandler readHandler = new ReadHandler(_notifier);

            fileReader.FileRead += (fileReadEventArgs) =>
            {
                readHandler.HandleReadFile(fileReadEventArgs.FileInfo);
            };

            fileReader.Start();
        }

    }
}
