using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;


public class HomeController:ControllerBase
{
    public void Start(UserControllerContext context)
    {
        this._botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Hello").Wait();
        
        context.Session.Action = nameof(AuthController.LoginUserLogin);
        context.Session.Controller = (nameof(AuthController));
    }
    public override void HandleAction(UserControllerContext context)
    {
        if (context.Session.Action == nameof(this.Start))
            this.Start(context);

        this.Start(context);
    }

    public HomeController(ITelegramBotClient botClient) : base(botClient)
    {
        
    }
}