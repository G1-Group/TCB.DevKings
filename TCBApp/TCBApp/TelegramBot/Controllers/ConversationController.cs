using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class ConversationController:ControllerBase
{
    public ConversationController(ITelegramBotClient botClient) : base(botClient)
    {
    }

    public override void HandleAction(UserControllerContext context)
    {
        throw new NotImplementedException();
    }
}