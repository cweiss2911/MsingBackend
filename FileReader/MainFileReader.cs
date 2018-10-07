using Messages;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Configuration;

namespace FileReader
{
    public class MainFileReader
    {
        private static INotifier _notifier;

        static void Main(string[] args)
        {
            FileReaderConfigurator fileReaderConfigurator = new FileReaderConfigurator();
            FileReaderConfig fileReaderConfig = fileReaderConfigurator.ReadConfig();

            _notifier = new HttpNotifier(fileReaderConfig.NotificationTarget);

            //Environment.Exit(4919);

            //FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(fileReaderConfig.InputLocation);
            //fileSystemWatcher.Created += OnFileCreated;
            //fileSystemWatcher.EnableRaisingEvents = true;

            while (true)
            {
                Thread.Sleep(1000);
                var all = Directory.GetFiles(fileReaderConfig.InputLocation, "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < all.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(all[i]);

                    OnFileCreated(null, new FileSystemEventArgs(WatcherChangeTypes.Created, fileInfo.DirectoryName, fileInfo.Name));
                    //Console.WriteLine(all[i]);
                }
            }
        }

        private static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"got file {e.Name}");
            Message message = new Message()
            {
                Type = "success",
                Payload = e.Name,
            };

            _notifier.Notify(new JsonContent(message));

            Console.WriteLine("Deleting");
            File.Delete(e.FullPath);
            Console.WriteLine("Deleted");
        }
    }
}
