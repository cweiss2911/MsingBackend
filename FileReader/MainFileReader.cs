using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using FileReader.FileHandling;
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
        private static INotifier _fileReadNotifier;
        private static INotifier _fileContentNotifier;

        static void Main(string[] args)
        {
            FileReaderConfigurator fileReaderConfigurator = new FileReaderConfigurator();
            FileReaderConfig fileReaderConfig = fileReaderConfigurator.ReadConfig();

            //_notifier = new HttpNotifier(fileReaderConfig.NotificationTarget);
            _fileReadNotifier = new KafkaNotifier(fileReaderConfig.KafkaServerAddress, fileReaderConfig.FileReadTopicName);
            _fileContentNotifier = new KafkaNotifier(fileReaderConfig.KafkaServerAddress, fileReaderConfig.FileContentTopicName);

            IFileHandler fileHandler = new FileHandlerImplementation(fileReaderConfig.ProcessedLocation);
            IFileReader fileReader = new PoorMansFileReader(fileReaderConfig.InputLocation, fileHandler);
            //IReadHandler readHandler = new SendAndDeleteHandler(_fileReadNotifier);

            Console.WriteLine($"{fileReaderConfig.ProcessedLocation}");

            IReadHandler readHandler = new SendAndMoveHandler(_fileReadNotifier, _fileContentNotifier, fileHandler);

            fileReader.FileRead += (fileReadEventArgs) =>
            {
                readHandler.HandleReadFile(fileReadEventArgs.FileInfo);
            };

            fileReader.Start();
        }

    }
}
