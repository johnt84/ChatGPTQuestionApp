using Microsoft.AspNetCore.Components;

namespace ChatGPTQuestionBlazorServerApp.Pages.ChatGPTQuestion
{
    public partial class ChatGPTQuestionEntry
    {
        private string QuestionForChatGPT { get; set; } = string.Empty;

        private string ErrorMessage { get; set; } = string.Empty;

        private string answerFromChatGPT = string.Empty;

        private bool buttonClicked = false;

        private bool answerReceived = false;

        private bool isValidQuestion = false;

        private async Task AskChatGPTAQuestionAsync()
        {
            ErrorMessage = string.Empty;
            buttonClicked = true;
            answerReceived = false;

            if (string.IsNullOrWhiteSpace(QuestionForChatGPT))
            {
                ErrorMessage = "Please enter a valid question for Chat GPT";

                isValidQuestion = false;

                return;
            }

            isValidQuestion = true;

            try
            {
                answerFromChatGPT = await ChatGPTCompletionService.PerformTextCompletion(QuestionForChatGPT);
                answerReceived = true;
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                isValidQuestion = false;
            }
        }

        private void ClearQuestion()
        {
            QuestionForChatGPT = string.Empty;
            answerFromChatGPT = string.Empty;
            ErrorMessage = string.Empty;
            ErrorMessage = string.Empty;
            buttonClicked = false;
            answerReceived = false;
        }
    }
}