using System.Net;

namespace TCBApp.WebApi;

public class WebHost
{
    protected HttpListener _httpListener;

    public WebHost()
    {
        _httpListener = new HttpListener();
    }
    
}