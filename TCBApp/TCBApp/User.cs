using System.Reflection.Metadata.Ecma335;

namespace TCBApp;

public class User
{
    public long UserId  { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public long   TelegramClientId { get; set; }
    
}