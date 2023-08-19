using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCBApp.Models;

[Table("users")]
public class User : ModelBase
{
    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("telegram_client_id")]
    public long   TelegramClientId { get; set; }

    //relation
    [NotMapped]
    public virtual Client Client { get; set; }
    [Column("signed")]
    public bool Signed { get; set; }
    
    [Column("last_login_date")]
    public DateTime LastLoginDate { get; set; }
}