using TCBApp.WebApi.Attribute.Enums;

namespace TCBApp.WebApi.Attribute;

public abstract class HttpMethod:System.Attribute
{
    public abstract HttpType Method { get; }
    public string Path { get; set; }
}