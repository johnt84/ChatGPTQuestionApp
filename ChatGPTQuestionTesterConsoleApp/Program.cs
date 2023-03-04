using ChatGPTEngine.Service;
using ChatGPTEngine.Service.API;
using ChatGPTShared;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                               .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var config = builder.Build();

Int32.TryParse(config["MaxTokens"], out int maxTokens);

var chatGPTEngineInput = new ChatGPTEngineInput()
{
    ChatGPTAPIURL = config["ChatGPTAPIURL"],
    SecretToken = config["SecretToken"],
    MaxTokens = maxTokens,
    Model = config["Model"],
};

var httpClient = new HttpClient();

httpClient.BaseAddress = new Uri(chatGPTEngineInput.ChatGPTAPIURL);

var httpAPIClient = new HttpAPIClient(httpClient, chatGPTEngineInput);

var chatGPTCompletionService = new ChatGPTCompletionService(httpAPIClient, chatGPTEngineInput);

bool isAnotherQuestionForChatGPT = true;

while(isAnotherQuestionForChatGPT)
{
    string questionForChatGPT = string.Empty;

    while (string.IsNullOrWhiteSpace(questionForChatGPT))
    {
        Console.WriteLine("\nWhat is your question for ChatGPT?");
        questionForChatGPT = Console.ReadLine();
    }
       
    string answerFromChatGPT = await chatGPTCompletionService.PerformTextCompletion(questionForChatGPT);

    Console.WriteLine($"\nAnswer from ChatGPT is: {answerFromChatGPT}");

    string anotherQuestionForChatGPT = string.Empty;

    var yOrN = new List<string>() 
    { 
        YesOrNo.y.ToString(), 
        YesOrNo.n.ToString() 
    };

    while (!yOrN.Contains(anotherQuestionForChatGPT.ToLower()))
    {
        Console.WriteLine($"\nWould you like to ask Chat GPT another question (y/n)?");
        anotherQuestionForChatGPT = Console.ReadLine();
    }
       

    isAnotherQuestionForChatGPT = anotherQuestionForChatGPT.ToLower() == YesOrNo.y.ToString();
}


Console.WriteLine("\nPress any key to continue...");
Console.ReadLine();

enum YesOrNo
{
    y,
    n
};