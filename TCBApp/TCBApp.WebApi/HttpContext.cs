

using System.Net;

namespace TCBApp.WebApi;

public class HttpContext
{
    public HttpListenerRequest Request { get; set; }
    public HttpListenerResponse Response { get; set; }
    public string QueryString { get; set; }
    public Dictionary<string,string> Params { get; set; }
   
}