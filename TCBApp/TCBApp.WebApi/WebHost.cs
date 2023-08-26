using System.Net;

namespace TCBApp.WebApi;

public class WebHost
{
    private readonly IoCContainer _container;
    protected HttpListener _httpListener;

    public WebHost(IoCContainer container)
    {
        _container = container;
        _httpListener = new HttpListener();
    }
    
    
}