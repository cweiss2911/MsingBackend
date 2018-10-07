using FileReader.FileReader;
using FileReader.FileReadHandler;

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
