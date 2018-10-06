using Messages;

namespace FileReader
{
    public interface INotifier
    {
        void Notify(JsonContent jsonContent);
    }
}