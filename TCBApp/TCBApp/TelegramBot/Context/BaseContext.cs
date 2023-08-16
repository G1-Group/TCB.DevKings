using Telegram.Bot.Types;

namespace TCBApp.TelegramBot;

public class BaseContext
{
    public Update Update { get; set; }
    public Message Message => Update?.Message;
}