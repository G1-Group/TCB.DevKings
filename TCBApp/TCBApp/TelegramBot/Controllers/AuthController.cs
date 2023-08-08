using System.Reflection.Metadata.Ecma335;
using TCBApp.Models;
using TCBApp.Services;
using Telegram.Bot;

namespace TCBApp.TelegramBot.Controllers;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    private string login = null;
    private string phonenumber = null;
    private string password;

    public AuthController(ITelegramBotClient botClient, AuthService authService) : base(botClient)
    {
        _authService = authService;
    }

    public async Task LoginUserStart(UserControllerContext context)
    {
        await _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Please Enter Login to Sign In");
        context.Session.Action = nameof(LoginUserLogin);
    }

    public async Task LoginUserLogin(UserControllerContext context)
    {
        login = context.Update.Message.Text;

        await _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Enter your password: ");
        context.Session.Action = nameof(LoginUserPassword);
    }

    public async Task LoginUserPassword(UserControllerContext context)
    {
        var password = context.Update.Message.Text;
        _authService.Login(new UserRegstration
        {
            User = new User
            {
                PhoneNumber = login,
                Password = password
            },
            TelegramChatId = context.Update.Message.Chat.Id
        });
        await _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, $"Login: {login}\n Password: {password}");

        context.Session.Controller = null;
        context.Session.Action = null;
    }

    public async Task RegstrationUserStart(UserControllerContext context)
    {
        await _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id,
            "Enter phone number as \"+998900000000\"");
        context.Session.Action = "RegstrationUserStart";
    }

    public async Task RegstraionUserPhoneNumber(UserControllerContext context)
    {
        phonenumber = context.Update.Message.Text;
        await _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Please Enter your password");
        context.Session.Action = "RegstraionUserPhoneNumber";
    }

    public async Task RegstrationUserPassword(UserControllerContext context)
    {
        password = context.Update.Message.Text;
        await _botClient.SendTextMessageAsync(context.Update.Message.Text, "You Succesfully registired");
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
        context.Session.Action = "RegstrationUserPassword";
    }


    public override async void HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(RegstrationUserStart):
            {
                await RegstrationUserStart(context);
                break;
            }
            case nameof(RegstraionUserPhoneNumber):
            {
                await RegstraionUserPhoneNumber(context);

                break;
            }
            case nameof(RegstrationUserPassword):
            {
                await RegstrationUserPassword(context);


                break;
            }
            case nameof(LoginUserStart):
            {
                await LoginUserStart(context);
                break;
            }
            case nameof(LoginUserLogin):
            {
                await LoginUserLogin(context);
                break;
            }
            case nameof(LoginUserPassword):
            {
                await LoginUserPassword(context);
                break;
            }
        }

        return;
    }
}