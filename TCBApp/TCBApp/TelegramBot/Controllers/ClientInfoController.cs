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
    public async Task EditNickName(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Enter new nick name",context.MakeClientInfoReplyKeyboardMarkup());
        context.Session.Action = nameof(EditNickNameSave);
    }
    public async Task EditNickNameSave(UserControllerContext context)
    {
        var client = await _clientDataService.GetByIdAsync(context.Session.ClientId.Value);
        var newNickname = context.Message.Text ?? string.Empty;
        await _clientService.UpdateClientNickName(context.Session.ClientId.Value, newNickname);
        await context.SendBoldTextMessage("Susses",context.MakeClientInfoReplyKeyboardMarkup());
    }
    public async Task EditUserName(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Enter new user name",context.MakeClientInfoReplyKeyboardMarkup());
        context.Session.Action = nameof(EditUserNameSave);
    }
    public async Task EditUserNameSave(UserControllerContext context)
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
            case nameof(EditNickName):
                await this.EditNickName(context);
                break; 
            case nameof(EditNickNameSave):
                await this.EditNickNameSave(context);
                break;
            case nameof(EditUserName):
                await this.EditUserName(context);
                break; 
            case nameof(EditUserNameSave):
                await this.EditUserNameSave(context);
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
                    context.Session.Action = nameof(ClientInfoController.EditNickName);
                    break;
                case "Enter user name":
                    context.Session.Action = nameof(ClientInfoController.EditUserName);
                    break;
                case "Back":
                    context.Session.Controller = nameof(ClientDashboardController);
                    context.Session.Action = nameof(ClientDashboardController.Index);
                    return;
            }
        }
    }
}