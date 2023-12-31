using System.Net;
using System.Runtime.CompilerServices;
using TCBApp.WebApi.Interfaces;
using TCBApp.WebApi.Types;

namespace TCBApp.WebApi.Built_in_Middlewares;

public sealed class ResponseCloserMiddleware: IMiddleware
{
    public async Task ExecuteAsync(HttpContext context, NextHandlerDelegate? next)
    {
        await next();
        context.Response.Close();
    }
}