namespace TCBApp.TelegramBot.Controllers;

public class ConversationsController : ControllerBase
{
    public ConversationsController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
    }

    public async Task Index()
    {
        
    }
    protected override async Task HandleAction(UserControllerContext context)
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        throw new NotImplementedException();
    }
}