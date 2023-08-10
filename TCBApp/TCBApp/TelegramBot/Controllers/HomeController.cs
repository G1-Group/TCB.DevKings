using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TCBApp.TelegramBot.Controllers;


public class HomeController:ControllerBase
{
    public async Task Index(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Welcome English Conversations bot!!!");
    }
    protected override async Task HandleAction(UserControllerContext context)
    {
        // if (context.Session.Action == nameof(this.Index))
        //     await this.Index(context);
        
        await this.Index(context);
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        //Check commands
        if (message!.Type == MessageType.Text)
        {
            var text = message.Text;
            if (text is not null)
                switch (text)
                {
                    case "/start":
                        context.Session.Action = nameof(Index);
                        break;
                    case "/login":
                        context.Session.Controller = nameof(AuthController);
                        context.Session.Action = nameof(AuthController.LoginUserStart);
                        break;
                    case "/register":
                        context.Session.Controller = nameof(AuthController);
                        context.Session.Action = nameof(AuthController.RegistrationStart);
                        break;
                }
        }
        // throw new NotImplementedException();
    }

    // public async Task Login(UserControllerContext context)
    // {
    //     context.Session.Controller = nameof(AuthController);
    //     context.Session.Action = nameof(AuthController.LoginUserStart);
    //
    //     await context.Forward(this._controllerManager);
    // }
    //
    // public async Task Register(UserControllerContext context)
    // {
    //     context.Session.Controller = nameof(AuthController);
    //     context.Session.Action = nameof(AuthController.RegistrationStart);
    //
    //     await context.Forward(this._controllerManager);
    // }

    public HomeController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
        
    }
}