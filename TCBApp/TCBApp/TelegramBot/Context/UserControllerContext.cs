using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.TelegramBot;

public class UserControllerContext:BaseContext
{
    public Func<Task> TerminateSession { get; set; }
    public Session Session { get; set; }
}