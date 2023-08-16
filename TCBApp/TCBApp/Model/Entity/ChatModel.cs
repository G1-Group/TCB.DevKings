using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TCBApp.Models;
using TCBApp.Models.Enums;

[Table("chats")]
public class ChatModel : ModelBase
{
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [Column("from_id")]
    public long FromId { get; set; }
    [NotMapped]
    public Client From { get; set; }
    [Column("to_id")]
    public long ToId { get; set; }
    [NotMapped]
    public Client To { get; set; }
    
    [Column("state")]
    public ChatState State { get; set; }

    [NotMapped]
    public List<Message> Messages { get; set; }
    
}