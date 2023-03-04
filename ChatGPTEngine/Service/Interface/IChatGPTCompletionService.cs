using ChatGPTShared;

namespace ChatGPTEngine.Service.Interface
{
    public interface IChatGPTCompletionService
    {
        Task<string> PerformTextCompletion(string questionForChatGPT);
    }
}
