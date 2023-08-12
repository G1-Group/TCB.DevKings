using TCBApp.Services;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class AdminController:ControllerBase
{

    public AdminController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
    }

    protected async override Task HandleAction(UserControllerContext context)
    {
    }

    protected override Task HandleUpdate(UserControllerContext context)
    {
        throw new NotImplementedException();
    }
}