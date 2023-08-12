using TCBApp.Services;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class ConversationController:ControllerBase
{
    private ConversationService _conversationService;
    public ConversationController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
        _conversationService = new ConversationService();
        
    }

    protected override Task HandleAction(UserControllerContext context)
    {
        throw new NotImplementedException();
    }

    protected override Task HandleUpdate(UserControllerContext context)
    {
        throw new NotImplementedException();
    }
}