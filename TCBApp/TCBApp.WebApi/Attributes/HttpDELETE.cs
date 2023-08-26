using TCBApp.WebApi.Attribute.Enums;

namespace TCBApp.WebApi.Attribute;

public class HttpDELETE:HttpMethod
{
    public override HttpType Method => HttpType.DELETE;
}