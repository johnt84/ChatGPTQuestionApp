namespace ChatGPTShared
{
    public class ChatGPTEngineInput
    {
        public string ChatGPTAPIURL { get; set; }
        public string SecretToken { get; set; }
        public int MaxTokens { get; set; }
        public string Model { get; set; }
    }
}
