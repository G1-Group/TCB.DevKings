using System.Net;
using System.Text.Json;

namespace TCBApp.WebApi.Extensions;

public static class HttpContextExtension
{
    public static async Task<T> ConvertByModel<T>(this HttpContext context,HttpWebRequest requestBody)
    {
        using (StreamReader reader = new StreamReader(context.Request.InputStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}