using TCBApp.TelegramBot;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public abstract class ControllerBase
{
    protected readonly ITelegramBotClient _botClient = TelegramBot._client;
    protected readonly ControllerManager.ControllerManager _controllerManager;

    public ControllerBase(ControllerManager.ControllerManager controllerManager)
    {
        _controllerManager = controllerManager;
    }
    protected abstract Task HandleAction(UserControllerContext context);
    protected abstract Task HandleUpdate(UserControllerContext context);

    public async Task Handle(UserControllerContext context)
    {
        await this.HandleUpdate(context);
        await this.HandleAction(context);
    }

}