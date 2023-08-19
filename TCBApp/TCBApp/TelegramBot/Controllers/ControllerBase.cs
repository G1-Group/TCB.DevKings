using TCBApp.TelegramBot;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TCBApp.TelegramBot.Controllers;

public abstract class ControllerBase
{
    protected readonly ITelegramBotClient _botClient = TelegramBot._client;
    protected readonly ControllerManager.ControllerManager _controllerManager;

    protected Message? message = null;

    public ControllerBase(ControllerManager.ControllerManager controllerManager)
    {
        _controllerManager = controllerManager;
    }
    protected abstract Task HandleAction(UserControllerContext context);
    protected abstract Task HandleUpdate(UserControllerContext context);

    public async Task Handle(UserControllerContext context)
    {
        if (context.Update.Message?.ViaBot is not null || context.Update.Type == UpdateType.ChosenInlineResult)
            return;
        SetUpdateMessage(context);
        string oldController = context.Session.Controller;
        await this.HandleUpdate(context);
        if (oldController != context.Session.Controller)
            await context.Forward(this._controllerManager);
        else
            await this.HandleAction(context);
    }

    public async Task RedirectToIndex(UserControllerContext context)
    {
        if (message is not null)
        {
            message.Text = null;
            await context.Forward(this._controllerManager);
        }
        
    }

    private void SetUpdateMessage(UserControllerContext context)
    {
        if (context.Update.Type is UpdateType.Message)
        {
            this.message = context.Update.Message;
            return;
        }

        this.message = null;
    }

}