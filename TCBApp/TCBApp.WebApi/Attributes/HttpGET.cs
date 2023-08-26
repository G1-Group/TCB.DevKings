using TCBApp.WebApi.Attribute.Enums;

namespace TCBApp.WebApi.Attribute;

public class HttpGET:HttpMethod
{
    public override HttpType Method => HttpType.GET;
}