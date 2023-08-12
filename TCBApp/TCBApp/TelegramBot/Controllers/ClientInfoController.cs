using TCBApp.Models;
using TCBApp.Services;
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
        Client client = await _clientDataService.GetById(context.Session.ClientId.Value);
        string userName = client.UserName is not null ? client.UserName: null;
        string nickName = client.Nickname is not null ? client.Nickname: null;
        string premium = client.IsPremium is true ? "Active" : "Not Active";
        string sendClientInfo = $"| Name : {userName} \n| Nick name : {nickName} \n| Premium : {premium}";
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