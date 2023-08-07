using TCBApp.Models;
using TCBApp.Services;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class AuthController:ControllerBase
{
    private readonly AuthService _authService;

    private string login = null;


    public AuthController(ITelegramBotClient botClient, AuthService authService): base(botClient)
    {
        _authService = authService;
    }
    public void LoginUserLogin(ControllerContext context)
    {
        login = context.Update.Message.Text;

        _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Enter your password: ").Wait();
        context.Session.Action = "LoginUserPassword";
    }

    public void LoginUserPassword(ControllerContext context)
    {
        var password = context.Update.Message.Text;
        _authService.Login(new UserRegstration
        {
            User =new User
            {
                PhoneNumber = login,
                Password =password
            },
            TelegramChatId =context.Update.Message.Chat.Id 
        });
    }

    public override void HandleAction(ControllerContext context)
    {
        if (context.Session.Action == nameof(this.LoginUserLogin))
            this.LoginUserLogin(context);
        else if (context.Session.Action == nameof(this.LoginUserPassword))
            this.LoginUserPassword(context);
    }
}