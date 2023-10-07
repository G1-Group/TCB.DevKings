using TCBApp.Models;
using TCBApp.Services;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot.Controllers;

public class ClientInfoController :ControllerBase
{
    private readonly ClientDataService _clientDataService;

    public ClientInfoController(ControllerManager.ControllerManager controllerManager,ClientDataService clientDataService) : base(controllerManager)
    {
        _clientDataService = clientDataService;
    }

    public async Task Index(UserControllerContext context)
    {
        var client = await _clientDataService.GetByIdAsync(context.Session.ClientId.Value);
        var user = client.User;
        string sendClientInfo = $"Phone: {user.PhoneNumber}\n" +
                                $"Password: {user.Password}\n" +
                                $"Nickname: {client.Nickname ?? null}\n" +
                                $"Username: {client.UserName?? null}";
        await context.SendBoldTextMessage(sendClientInfo,new ReplyKeyboardMarkup(new KeyboardButton("Back"))
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        });
    }

    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await this.Index(context);
                break;
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (message.Type is MessageType.Text)
        {
            if (message.Text == "Back")
            {
                context.Session.Controller = nameof(BoardController);
                context.Session.Action = nameof(BoardController.Index);
                return;
            }
        }
    }
}