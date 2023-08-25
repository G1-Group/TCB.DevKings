using System.Net;
using TCBApp.WebApi.Types;

namespace TCBApp.WebApi.Interfaces;

public interface IMiddleware
{
    public Task ExecuteAsync(HttpContext context, NextHandlerDelegate? next);
}