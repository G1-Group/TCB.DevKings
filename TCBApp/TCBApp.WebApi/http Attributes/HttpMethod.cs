using TCBApp.WebApi.http_Attributes.Enums;

namespace TCBApp.WebApi.http_Attributes;

public abstract class HttpMethod:Attribute
{
    public abstract string Method { get; set; }
    public abstract HttpType Http { get; }
}