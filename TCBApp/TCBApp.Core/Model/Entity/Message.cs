using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCBApp.Models.Enums;

namespace TCBApp.Models;

[Table("messages")]
public class Message : ModelBase
{
    [Column("from_id")]
    public long FromId { get; set; }
    
    [NotMapped]
    public virtual Client Client { get; set; }
    
    [Column("content")]
    public object Content { get; set; }
    [Column("conversation_id")]
    public long? ConversationId { get; set; }
    [NotMapped]
    public virtual ChatModel? Conversation { get; set; }
    
    [Column("board_id")]
    public long? BoardId { get; set; }
    [NotMapped]
    public virtual BoardModel? Board { get; set; }
    
    [Column("message_type")]
    public MessageType MessageType { get; set; }
    [Column("message_status")]
    public MessageStatus MessageStatus { get; set; }
}