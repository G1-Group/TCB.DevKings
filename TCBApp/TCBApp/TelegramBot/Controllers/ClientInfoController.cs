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
    private readonly ClientService _clientService;

    public ClientInfoController(ControllerManager.ControllerManager controllerManager,ClientDataService clientDataService, ClientService clientService) : base(controllerManager)
    {
        _clientDataService = clientDataService;
        _clientService = clientService;
    }

    public async Task Index(UserControllerContext context)
    {
        var client = await _clientDataService.GetByIdAsync(context.Session.ClientId.Value);
        var user = client.User;
        string sendClientInfo = $"Phone: {user.PhoneNumber}\n" +
                                $"Password: {user.Password}\n" +
                                $"Nickname: {client.Nickname ?? null}\n" +
                                $"Username: {client.UserName?? null}";
        await context.SendBoldTextMessage(sendClientInfo,context.MakeClientInfoReplyKeyboardMarkup());
    }
    public async Task EnterNickName(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Enter new nick name",context.MakeClientInfoReplyKeyboardMarkup());
        context.Session.Action = nameof(EnterNickNameSave);
    }
    public async Task EnterNickNameSave(UserControllerContext context)
    {
        var client = await _clientDataService.GetByIdAsync(context.Session.ClientId.Value);
        var newNickname = context.Message.Text ?? string.Empty;
        await _clientService.UpdateClientNickName(context.Session.ClientId.Value, newNickname);
        await context.SendBoldTextMessage("Susses",context.MakeClientInfoReplyKeyboardMarkup());
    }
    public async Task EnterUserName(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Enter new user name",context.MakeClientInfoReplyKeyboardMarkup());
        context.Session.Action = nameof(EnterUserNameSave);
    }
    public async Task EnterUserNameSave(UserControllerContext context)
    {
        var client = await _clientDataService.GetByIdAsync(context.Session.ClientId.Value);
        var newUserName = context.Message.Text ?? string.Empty;
        await _clientService.UpdateClientUserName(context.Session.ClientId.Value,newUserName);
        await context.SendBoldTextMessage("Susses",context.MakeClientInfoReplyKeyboardMarkup());
    }
    

    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await this.Index(context);
                break;
            case nameof(EnterNickName):
                await this.EnterNickName(context);
                break; 
            case nameof(EnterNickNameSave):
                await this.EnterNickNameSave(context);
                break;
            case nameof(EnterUserName):
                await this.EnterUserName(context);
                break; 
            case nameof(EnterUserNameSave):
                await this.EnterUserNameSave(context);
                break;
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (message.Type is MessageType.Text)
        {
            string text = context.Message?.Text;
            switch (text)
            {
                case "Enter nick name":
                    context.Session.Action = nameof(ClientInfoController.EnterNickName);
                    break;
                case "Enter user name":
                    context.Session.Action = nameof(ClientInfoController.EnterUserName);
                    break;
                case "Back":
                    context.Session.Controller = nameof(ClientDashboardController);
                    context.Session.Action = nameof(ClientDashboardController.Index);
                    return;
            }
        }
    }
}