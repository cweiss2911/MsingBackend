using Messages;
using System.Net.Http;

namespace FileReader
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

        public void Notify(JsonContent jsonContent)
        {
            _httpClient.PostAsync($"{_notificationTarget}/api/message", jsonContent);            
        }
    }
}