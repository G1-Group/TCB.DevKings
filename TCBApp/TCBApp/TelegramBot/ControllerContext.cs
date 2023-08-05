using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.TelegramBot;

public class ControllerContext
{
    public Update Update { get; set; }
    public Session Session { get; set; }
}