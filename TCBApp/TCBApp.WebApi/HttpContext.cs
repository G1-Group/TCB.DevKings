using System.Net;

namespace TCBApp.WebApi;

public class HttpContext
{
    public WebRequest Request { get; set; }
    public WebResponse Response { get; set; }
    public string QueryString { get; set; }
    public Dictionary<string,string> Params { get; set; }
   
}