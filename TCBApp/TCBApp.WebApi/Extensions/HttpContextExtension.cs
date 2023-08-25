using System.Net;
using System.Text.Json;

namespace TCBApp.WebApi.Extensions;

public static class HttpContextExtension
{
    public static async Task<T> ConvertByModel<T>(this HttpContext context)
    {
        using (StreamReader reader = new StreamReader(context.Request.InputStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }

    public static async Task<string> Convert(this HttpContext context)
    {
        using (StreamReader reader = new StreamReader(context.Request.InputStream))
        {
            return await reader.ReadToEndAsync();
        }
    }
}