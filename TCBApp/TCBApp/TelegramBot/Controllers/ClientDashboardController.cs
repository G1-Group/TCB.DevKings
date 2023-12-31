using System.Net.Security;
using TCBApp.Models;
using TCBApp.Services;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot.Controllers;

public class ClientDashboardController : ControllerBase
{
    private readonly ClientDataService _clientDataService;
    private readonly AuthService _authService;

    public ClientDashboardController(ControllerManager.ControllerManager controllerManager, ClientDataService clientDataService, AuthService authService) : base(controllerManager)
    {
        _clientDataService = clientDataService;
        _authService = authService;
    }

    public async Task Index(UserControllerContext context)
    {
        Client? client = null;
        if (context.Session.ClientId != null)
             client = await _clientDataService.GetByIdAsync(context.Session.ClientId!.Value);

        if (client is not null)
        {
            await context.SendBoldTextMessage(
                $"Salom, Xush kelibsiz {(string.IsNullOrEmpty(client.Nickname) ? context.Update.Message?.Chat.FirstName : client.Nickname)}!",
                replyMarkup: context.MakeClientDashboardReplyKeyboardMarkup());
        }
        else
        {
            await context.SendErrorMessage("Foydalanuvchi ma'lumotlari topilmadi❌");
        }

    }

    public async Task LogOut(UserControllerContext context)
    {
        await _authService.Logout(context.Session.User.Id);
        await context.TerminateSession();
        await context.SendBoldTextMessage("Logged out", replyMarkup: new ReplyKeyboardRemove());
    }
    
    
    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await this.Index(context);
                break;
            case nameof(LogOut):
                await this.LogOut(context);
                break;
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (context.Message?.Type is MessageType.Text)
        {
            string text = context.Message?.Text;
            switch (text)
            {
                case "Boards":
                    context.Session.Controller = nameof(BoardController);
                    context.Session.Action = nameof(BoardController.Index);
                    break;
                case "Log out":
                    context.Session.Action = nameof(LogOut);
                    break;
                case "Settings⚙️":
                    context.Session.Controller = nameof(SettingsController);
                    context.Session.Action = nameof(SettingsController.Index);
                    break;
                case "User Info":
                    context.Session.Controller = nameof(ClientInfoController);
                    context.Session.Action = nameof(ClientInfoController.Index);
                    break;
                case "Conversations":
                    context.Session.Controller = nameof(ConversationsController);
                    context.Session.Action = nameof(ConversationsController.Index);                    
                    break;
                
            }
        }
    }
}