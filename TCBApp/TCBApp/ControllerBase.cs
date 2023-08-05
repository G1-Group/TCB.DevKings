using TCBApp.TelegramBot;
using Telegram.Bot;

namespace TCBApp;

public abstract class ControllerBase
{
    protected readonly ITelegramBotClient _botClient;

    public ControllerBase(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }
    public abstract void HandleAction(ControllerContext context);
}