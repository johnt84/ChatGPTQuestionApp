using ChatGPTEngine.Service.API.Interface;
using ChatGPTShared;
using System.Net.Http.Headers;

namespace ChatGPTEngine.Service.API
{
    public class HttpAPIClient : IHttpAPIClient
    {
        public HttpClient _Client { get; }
        private readonly ChatGPTEngineInput _chatGPTEngineInput;

        public HttpAPIClient(HttpClient httpClient, ChatGPTEngineInput chatGPTEngineInput)
        {
            _chatGPTEngineInput = chatGPTEngineInput;
            _Client = httpClient;

            _Client.BaseAddress = new Uri(chatGPTEngineInput.ChatGPTAPIURL);
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {chatGPTEngineInput.SecretToken}");
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent)
        {
            var response = await _Client.PostAsync(url, httpContent);

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}