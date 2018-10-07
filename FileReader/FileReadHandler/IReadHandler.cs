using System.IO;

namespace FileReader.FileReadHandler
{
    public interface IReadHandler
    {
        void HandleReadFile(FileInfo fileInfo);
    }
}