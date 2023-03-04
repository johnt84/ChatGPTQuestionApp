namespace ChatGPTQuestionBlazorServerApp.Pages.ChatGPTQuestion
{
    public partial class ChatGPTQuestionEntry
    {
        private string questionForChatGPT = string.Empty;

        private string answerFromChatGPT = string.Empty;

        private bool buttonClicked = false;

        private bool answerReceived = false;

        public async Task AskChatGPTAQuestionAsync()
        {
            buttonClicked = true;
            answerReceived = false;
            answerFromChatGPT = await ChatGPTCompletionService.PerformTextCompletion(questionForChatGPT);
            answerReceived = true;
        }
    }
}