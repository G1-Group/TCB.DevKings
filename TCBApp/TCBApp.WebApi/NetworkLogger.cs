namespace TCBApp.WebApi;

public class NetworkLogger
{
    public void AddLog(HttpContext context)
    {
        Console.WriteLine($"Request query: {context.Request.QueryString}  Responce status code : {context.Response.StatusCode} " +
                          $" Time: {DateTime.Now}");
    }
}