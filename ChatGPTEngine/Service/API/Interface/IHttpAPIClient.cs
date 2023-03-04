namespace ChatGPTEngine.Service.API.Interface
{
    public interface IHttpAPIClient
    {
        Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent);
    }
}
