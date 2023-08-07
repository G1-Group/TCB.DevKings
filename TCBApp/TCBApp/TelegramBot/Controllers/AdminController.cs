using TCBApp.Services;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class AdminController:ControllerBase
{

    public AdminController(ITelegramBotClient botClient) : base(botClient)
    {
    }

    public override void HandleAction(ControllerContext context)
    {
    }
    
    
}