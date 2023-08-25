using System.Net;
using System.Text.Json;

namespace TCBApp.WebApi.Extensions;

public static class ResponseExtensions
{
    public static async Task<T> ConvertAsync<T>(this HttpListenerResponse response )
    {
        
        using (StreamReader reader = new StreamReader(response.OutputStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}