using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.TelegramBot;

public class UserControllerContext:BaseContext
{
   
    public Session Session { get; set; }
}