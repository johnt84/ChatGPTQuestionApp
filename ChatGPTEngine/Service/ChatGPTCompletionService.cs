using ChatGPTEngine.Service.API.Interface;
using ChatGPTEngine.Service.Interface;
using ChatGPTShared;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ChatGPTEngine.Service
{
    public class ChatGPTCompletionService : IChatGPTCompletionService
    {
        private readonly IHttpAPIClient _httpAPIClient = null;
        private readonly ChatGPTEngineInput _chatGPTInput = null;
        public ChatGPTCompletionService(IHttpAPIClient httpAPIClient, ChatGPTEngineInput chatGPTInput)
        {
            _httpAPIClient = httpAPIClient;
            _chatGPTInput = chatGPTInput;
        }

        public async Task<string> PerformTextCompletion(string questionForChatGPT)
        {
            string answerFromChatGPT = string.Empty;
            
            var completionRequest = new CompletionRequest()
            {
                Model = _chatGPTInput.Model,
                Prompt = questionForChatGPT,
                MaxTokens = _chatGPTInput.MaxTokens,
            };

            var completionResponse = await PostCompletionRequest(completionRequest);

            if (completionResponse is not null)
            {
                answerFromChatGPT = completionResponse.Choices?[0]?.Text;
            }

            return answerFromChatGPT;
        }

        private async Task<CompletionResponse> PostCompletionRequest(CompletionRequest completionRequest)
        {
            string requestString = JsonSerializer.Serialize(completionRequest);

            var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

            var response = await _httpAPIClient.PostAsync("completions", httpContent);

            return await response.Content.ReadFromJsonAsync<CompletionResponse>();
        }
    }
}
