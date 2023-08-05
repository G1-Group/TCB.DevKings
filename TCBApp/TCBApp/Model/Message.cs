using System.ComponentModel;
using TCBApp.Models.Enums;

namespace TCBApp.Models;

public class Message
{
    [Description("from_id")]
    public long FromId { get; set; }
    [Description("message")]
    public object _Message { get; set; }
    [Description("chat_id")]
    public long ChatId { get; set; }
    [Description("board_id")]
    public long BoardId { get; set; }
    [Description("message_type")]
    public MessageType MessageType { get; set; }
    [Description("message_status")]
    public MessageStatus MessageStatus { get; set; }
}