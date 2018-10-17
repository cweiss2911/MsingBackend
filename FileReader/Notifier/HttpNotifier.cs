using Messages;
using System.Net.Http;

namespace FileReader.Notifier
{
    internal class HttpNotifier : INotifier
    {
        private HttpClient _httpClient;
        private string _notificationTarget;

        public HttpNotifier(string notificationTarget)
        {
            _httpClient = new HttpClient();
            _notificationTarget = notificationTarget;
        }

        public void Notify(Message message)
        {
            JsonContent jsonContent = new JsonContent(message);
            _httpClient.PostAsync($"{_notificationTarget}/api/message", jsonContent);
        }
    }
}