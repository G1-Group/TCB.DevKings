using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCBApp.Models;

[Table("users")]
public class User
{
    [Column("id")]
    public long UserId  { get; set; }
    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("telegram_client_id")]
    public long   TelegramClientId { get; set; }
    
}