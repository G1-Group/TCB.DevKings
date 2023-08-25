using System.Net;
using System.Runtime.CompilerServices;
using TCBApp.WebApi.Interfaces;
using TCBApp.WebApi.Types;

namespace TCBApp.WebApi;

public class LogMiddleware : IMiddleware
{
    public async Task ExecuteAsync(HttpContext context, NextHandlerDelegate? next)
    {
        // context. write anything
        //in-coming context
        Console.WriteLine("In-coming context");
        if (next is not null)
            await next();

        await Task.Delay(5000);
        Console.WriteLine("out-coming context");
        //out-coming context 
    }
}