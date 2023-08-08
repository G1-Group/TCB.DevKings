using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TCBApp.TelegramBot.Controllers;


public class HomeController:ControllerBase
{
    public async Task Start(UserControllerContext context)
    {
        await context.SendTextMessage("Welcome!");
    }
    protected override async Task HandleAction(UserControllerContext context)
    {
        // if (context.Session.Action == nameof(this.Start))
        //     await this.Start(context);
        //
        // await this.Start(context);
        
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        //Check commands
        var update = context.Update;
        if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
        {
            var text = update.Message.Text;
            if (text is not null)
                switch (text)
                {
                    case "/start":
                        await this.Start(context);
                        break;
                    case "/login":
                        await this.Login(context);
                        break;
                    case "/register":
                        await this.Register(context);
                        break;
                }
        }
        // throw new NotImplementedException();
    }

    public async Task Login(UserControllerContext context)
    {
        context.Session.Controller = nameof(AuthController);
        context.Session.Action = nameof(AuthController.LoginUserStart);

        await context.Forward(this._controllerManager);
    }

    public async Task Register(UserControllerContext context)
    {
        context.Session.Controller = nameof(AuthController);
        context.Session.Action = nameof(AuthController.RegistrationStart);

        await context.Forward(this._controllerManager);
    }

    public HomeController(ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
        
    }
}