using Messages;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Configuration;

namespace FileReader
{
    public class Program
    {
        private static INotifier _notifier;

        static void Main(string[] args)
        {
            _notifier = new HttpNotifier();

            FileReaderConfigurator fileReaderConfigurator = new FileReaderConfigurator();
            FileReaderConfig fileReaderConfig = fileReaderConfigurator.ReadConfig();
            
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(fileReaderConfig.InputLocation);
            fileSystemWatcher.Created += OnFileCreated;
            fileSystemWatcher.EnableRaisingEvents = true;

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static void OnFileCreated(object sender, FileSystemEventArgs e)
        {            
            Message message = new Message()
            {
                Type = "success",
                Payload = e.Name,
            };

            _notifier.Notify(new JsonContent(message));
            

            File.Delete(e.FullPath);
        }
    }
}
