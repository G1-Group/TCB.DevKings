using System.Net;

namespace TCBApp.WebApi;

public class WebHost
{
    protected HttpListener _httpListener;
    protected List<ControllerBase> _controllers { get; set; }

    public WebHost()
    {
        _controllers = new List<ControllerBase>();
        _httpListener = new HttpListener();
    }
    
}