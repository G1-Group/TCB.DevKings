using TCBApp.WebApi.http_Attributes.Enums;

namespace TCBApp.WebApi.http_Attributes;

public class HttpDELETE:HttpMethod
{
    public override string Method { get; set; }
    public override HttpType Http { get; }
}