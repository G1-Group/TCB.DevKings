using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class BoardController:ControllerBase
{
    public BoardController(ITelegramBotClient botClient) : base(botClient)
    {
    }

    public override void HandleAction(ControllerContext context)
    {
     
    }
}