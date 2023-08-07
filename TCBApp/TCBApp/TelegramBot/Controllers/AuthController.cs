using TCBApp.Models;
using TCBApp.Services;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class AuthController:ControllerBase
{
    private readonly AuthService _authService;
 
    private string login = null;
    private string phonenumber = null;
    private string password;

    public AuthController(ITelegramBotClient botClient, AuthService authService): base(botClient)
    {
        _authService = authService;
    }
    public void LoginUserLogin(ControllerContext context)
    {
        login = context.Update.Message.Text;

        _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Enter your password: ").Wait();
        context.Session.Action = "LoginUserLogin";
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
        context.Session.Action = "LoginUserPassword";
       
    }

    public void RegstrationUserRegistration(ControllerContext context)
    {
        _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Enter phone number as \"+998900000000\"");
        context.Session.Action = "RegstrationUserRegistration";

    }

    public void RegstraionUserPhoneNumber(ControllerContext context)
    {
        phonenumber = context.Update.Message.Text;
        _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Please Enter your password");
        context.Session.Action = "RegstraionUserPhoneNumber";

    }

    public void RegstrationPassword(ControllerContext context)
    {
        password = context.Update.Message.Text;
        _botClient.SendTextMessageAsync(context.Update.Message.Text, "You Succesfully registired");
        _authService.Registration(new UserRegstration()
        {
            User = new User()
            {
                PhoneNumber = phonenumber,
                Password = password,
                TelegramClientId = context.Update.Message.Chat.Id
            },
            TelegramChatId = context.Update.Message.Chat.Id
        });
        context.Session.Action = "RegstrationPassword";
    }

    
    public override void HandleAction(ControllerContext context)
    {
        if (context.Session.Action == nameof(this.RegstrationUserRegistration))
        {
            this.RegstrationUserRegistration(context);
        }
        else if(context.Session.Action==nameof(this.RegstraionUserPhoneNumber))
        {
            this.RegstraionUserPhoneNumber(context);
        }
        else if (context.Session.Action == nameof(this.RegstrationPassword))
        {
            this.RegstrationPassword(context);
        }

        else if (context.Session.Action == nameof(this.LoginUserLogin))
            this.LoginUserLogin(context);
        else if (context.Session.Action == nameof(this.LoginUserPassword))
            this.LoginUserPassword(context);
    }
}