namespace TCBApp.WebApi;

public interface IMiddleware
{
    ValueTask ExecuteAsync(HttpContext context);
}