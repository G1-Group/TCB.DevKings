
namespace TCBApp.WebApi.Attribute;

public class RouteAttribute: System.Attribute
{
    public string Path { get; set; }
    public string Method { get; set; }
}