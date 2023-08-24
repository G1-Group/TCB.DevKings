using System.Net;
using System.Text.Json;

namespace TCBApp.WebApi.Extensions;

public static class HttpContextExtension
{
    public static async Task<T> ConvertByModel<T>(this HttpContent content,HttpWebRequest requestBody)
    {
        using (StreamReader reader = new StreamReader(content.ReadAsStream()))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}