using Messages;
using System.Net.Http;

namespace FileReader
{
    internal class HttpNotifier : INotifier
    {
        private HttpClient _httpClient;

        public HttpNotifier()
        {
            _httpClient = new HttpClient();
        }

        public void Notify(JsonContent jsonContent)
        {
            _httpClient.PostAsync("http://localhost:50301/api/message", jsonContent);
        }
    }
}