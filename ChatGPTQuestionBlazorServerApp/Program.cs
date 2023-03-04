using ChatGPTEngine.Service;
using ChatGPTEngine.Service.API;
using ChatGPTEngine.Service.API.Interface;
using ChatGPTEngine.Service.Interface;
using ChatGPTShared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

Int32.TryParse(builder.Configuration["MaxTokens"], out int maxTokens);

var chatGPTEngineInput = new ChatGPTEngineInput()
{
    ChatGPTAPIURL = builder.Configuration["ChatGPTAPIURL"],
    SecretToken = builder.Configuration["SecretToken"],
    MaxTokens = maxTokens,
    Model = builder.Configuration["Model"],
};

builder.Services.AddSingleton(chatGPTEngineInput);
builder.Services.AddHttpClient<IHttpAPIClient, HttpAPIClient>();
builder.Services.AddSingleton<IChatGPTCompletionService, ChatGPTCompletionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
