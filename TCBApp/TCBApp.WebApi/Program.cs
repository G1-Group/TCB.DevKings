// See https://aka.ms/new-console-template for more information

using TCBApp.WebApi;
using TCBApp.WebApi.Built_in_Middlewares;

Console.WriteLine("Hello, World!");

var host = new WebHost();
host.Initialize("http://localhost:1771/");

host.Use<ResponseCloserMiddleware>();
host.Use<LogMiddleware>();

await host.StartAsync();