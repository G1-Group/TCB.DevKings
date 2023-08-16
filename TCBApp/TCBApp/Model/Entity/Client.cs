using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TCBApp.Models.Enums;
namespace TCBApp.Models;

[Table("clients")]
public class Client : ModelBase
{
    [Column("user_id")]
    public long  UserId { get; set; }
    [NotMapped]
    public User User { get; set; } 
    [Column("user_name")]
    public string  UserName { get; set; }
    [Column("nickname")]
    public string Nickname { get; set; }
    [Column("status")]
    public ClientStatus Status { get; set; }
    [Column("is_premium")]
    public bool IsPremium { get; set; }

    [NotMapped]
    public List<Message> Messages { get; set; }

    [NotMapped]
    public List<BoardModel> Boards { get; set; }
    [NotMapped]
    public List<ChatModel> ToConversations { get; set; }

    [NotMapped]
    public List<ChatModel> FromConversations { get; set; }
}