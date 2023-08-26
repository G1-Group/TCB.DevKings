using System.Net;
using System.Runtime.CompilerServices;
using TCBApp.WebApi.Interfaces;
using TCBApp.WebApi.Types;

namespace TCBApp.WebApi;

public class LogMiddleware : IMiddleware
{
    public async Task ExecuteAsync(HttpContext context, NextHandlerDelegate? next)
    {
        Console.WriteLine($"Ip: {context.Request.RemoteEndPoint} Request: {context.QueryString.ToString()}   " +
                          $"Response status code: {context.Response.StatusCode} Time: {DateTime.Now}  Method: {next.Method}");
    }
    
}