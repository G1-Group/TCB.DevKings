using System.Net;
using System.Text.Json;

namespace TCBApp.WebApi.Extensions;

public static class RequestExtension
{
    public static async Task<T> ConvertAsync<T>(this HttpListenerRequest request)
    {
        using (StreamReader reader = new StreamReader(request.InputStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}