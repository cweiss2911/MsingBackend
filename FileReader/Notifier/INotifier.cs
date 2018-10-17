using Messages;

namespace FileReader.Notifier
{
    public interface INotifier
    {
        void Notify(Message message);
    }
}