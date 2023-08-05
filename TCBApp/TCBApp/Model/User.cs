using System.ComponentModel;

namespace TCBApp.Models;

public class User
{
    [Description("user_id")]
    public long UserId  { get; set; }
    [Description("phone_number")]
    public string PhoneNumber { get; set; }
    [Description("password")]
    public string Password { get; set; }
    [Description("telegram_client_id")]
    public long   TelegramClientId { get; set; }
    
}