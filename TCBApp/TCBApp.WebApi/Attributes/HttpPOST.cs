using TCBApp.WebApi.Attribute.Enums;

namespace TCBApp.WebApi.Attribute;

public class HttpPOST:HttpMethod
{
    public override HttpType Method => HttpType.POST;
}