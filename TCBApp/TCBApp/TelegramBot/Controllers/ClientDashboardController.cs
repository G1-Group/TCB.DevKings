using TCBApp.Models;
using TCBApp.Services;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot.Controllers;

public class ClientDashboardController : ControllerBase
{
    private readonly ClientDataService _clientDataService;

    public ClientDashboardController(ControllerManager.ControllerManager controllerManager, ClientDataService clientDataService) : base(controllerManager)
    {
        _clientDataService = clientDataService;
    }

    public async Task Index(UserControllerContext context)
    {
        Client? client = null;
        if (context.Session.ClientId != null)
             client = await _clientDataService.GetById(context.Session.ClientId!.Value);

        if (client is not null)
        {
            await context.SendBoldTextMessage(
                $"Salom, Xush kelibsiz {(string.IsNullOrEmpty(client.Nickname) ? context.Update.Message.Chat.FirstName : client.Nickname)}!",
                replyMarkup: context.MakeClientDashboardReplyKeyboardMarkup());
        }
        else
        {
            await context.SendErrorMessage("Foydalanuvchi ma'lumotlari topilmadi❌");
        }

    }

    public async Task LogOut(UserControllerContext context)
    {
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
        if (message?.Type is MessageType.Text)
        {
            string text = message?.Text;
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
                    break;
            }
        }
    }
}