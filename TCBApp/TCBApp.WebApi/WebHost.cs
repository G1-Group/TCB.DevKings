using System.Net;
using TCBApp.WebApi.Interfaces;
using TCBApp.WebApi.Types;

namespace TCBApp.WebApi;

public class WebHost
{
    public IoCContainer _container { get; }
    protected HttpListener _httpListener;

    private LinkedList<Func<HttpContext, NextHandlerDelegate, Task>> middlewares;
    private List<IMiddleware> MiddlewareCollections = new List<IMiddleware>();
    

    public WebHost(IoCContainer? container = null)
    {
        _container = container ?? new IoCContainer();
        middlewares = new LinkedList<Func<HttpContext, NextHandlerDelegate, Task>>();
    }


    public void Initialize(string prefix)
    {
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add(prefix);
    }

    public async Task<NextHandlerDelegate> MakeNextHandlerChainingAsync(HttpContext context,
        LinkedListNode<Func<HttpContext, NextHandlerDelegate, Task>>? middleware)
    {
        if (middleware is null)
            return null;

        return async () =>
            await middleware.Value(context,
                await MakeNextHandlerChainingAsync(context, middleware.Next)
            );
    }

    public void Use<T>() where T:IMiddleware
    {
        var middleware = Activator.CreateInstance<T>();
        this.MiddlewareCollections.Add(middleware);
        middlewares.AddLast(middleware.ExecuteAsync);
    }
    
    public void Use(Func<HttpContext, NextHandlerDelegate, Task> middleware)
    {
        middlewares.AddLast(middleware);
    }

    private async Task AcceptContextAsync()
    {
        while (true)
        {
            var context = await _httpListener.GetContextAsync();
            var methodChain = MakeNextHandlerChainingAsync(HttpContext.FromListenerContext(context), middlewares.First).Result;
            ThreadPool.QueueUserWorkItem((state) => { methodChain().Wait(); });
        }
    }

    public async Task StartAsync()
    {
        _httpListener.Start();
        Console.WriteLine("Server is started on {0}.", _httpListener.Prefixes.ElementAt(0));
        await this.AcceptContextAsync();
    }
    public void RegisterMiddleware<T>() where T:IMiddleware
    {
        _container.Register<Interfaces.IMiddleware>();
    }
}