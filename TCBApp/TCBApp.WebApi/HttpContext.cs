

using System.Net;

namespace TCBApp.WebApi;

public class HttpContext
{
    public HttpListener Request { get; set; }
    public HttpListener Response { get; set; }
    public string QueryString { get; set; }
    public Dictionary<string,string> Params { get; set; }
   
}