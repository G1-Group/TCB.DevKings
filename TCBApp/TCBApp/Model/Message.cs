

using TCBApp.Models.Enums;

namespace TCBApp.Models;

public class Message
{
    public long FromId { get; set; }
    public object _Message { get; set; }
    public long ChatId { get; set; }
    public long BoardId { get; set; }
    public MessageType.MyEnum MessageType { get; set; }
}