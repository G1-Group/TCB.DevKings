using System.Linq.Expressions;

namespace TCBApp.Models;

public class UserLoginModel
{
    // public User User { get; set; }
    // public long TelegramChatId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}