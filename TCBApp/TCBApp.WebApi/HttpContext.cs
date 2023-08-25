using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;

namespace TCBApp.WebApi;

public class HttpContext
{
    public HttpListenerRequest Request { get; set; }
    public HttpListenerResponse Response { get; set; }
    public NameValueCollection QueryString => Request.QueryString;
    public IPrincipal? User { get; set; }

    public static HttpContext
        FromListenerContext(HttpListenerContext context) => new HttpContext()
    {
        Request = context.Request,
        Response = context.Response,
        User = context.User
    };
}