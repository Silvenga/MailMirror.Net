namespace MailMirror.Net.Piper
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using MailMirror.Net.Common.Models;

    public class Client
    {
        private readonly string _host;

        public Client(string host = null)
        {
            _host = host ?? "http://localhost:{Constants.Port}/";
        }

        public async Task SendAsync(string eml, string postfixQueueId, string recipient, string sender)
        {
            var message = new Message
            {
                Eml = eml,
                PostfixQueueId = postfixQueueId,
                Recipient = recipient,
                Sender = sender
            };

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync("/api/messages", message);
                response.EnsureSuccessStatusCode();
            }
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_host)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}