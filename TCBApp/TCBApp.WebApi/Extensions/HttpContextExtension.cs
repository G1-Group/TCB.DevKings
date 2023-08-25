using System.Net;
using System.Text.Json;

namespace TCBApp.WebApi.Extensions;

public static class HttpContextExtension
{
    public static async Task<T> ConvertByModelRerques<T>(this HttpContext context,HttpListenerRequest request)
    {
        using (StreamReader reader = new StreamReader(request.InputStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
    public static async Task<T> ConvertByModelResponse<T>(this HttpContext context,HttpListenerResponse response)
    {
        using (StreamReader reader = new StreamReader(response.OutputStream))
        {
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}