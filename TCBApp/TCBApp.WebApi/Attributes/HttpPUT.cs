using TCBApp.WebApi.Attribute.Enums;

namespace TCBApp.WebApi.Attribute;

public class HttpPUT:HttpMethod
{
    public override HttpType Method => HttpType.PUT;
}