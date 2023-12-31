﻿using System.Reflection.Metadata.Ecma335;
using TCBApp.Models;
using TCBApp.Services;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot.Controllers;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService, ControllerManager.ControllerManager controllerManager) : base(
        controllerManager)
    {
        _authService = authService;
    }

    public async Task LoginUserStart(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Please enter phone number : ",
            context.RequesPhoheNumberReplyKeyboardMarkup());

        context.Session.LoginData = new LoginSessionModel();
        context.Session.Action = nameof(LoginUserLogin);
    }

    private async Task LoginUserLogin(UserControllerContext context)
    {
        var login = context.Message?.Contact?.PhoneNumber;
        if (login is null)
        {
            await context.SendErrorMessage("Wrong phone number!", 400);
            return;
        }

        if (!login.StartsWith("+"))
            login = "+" + login;
        context.Session.LoginData.Login = login;
        await context.SendBoldTextMessage("Enter your password : ");
        context.Session.Action = nameof(LoginUserPassword);
    }

    private async Task LoginUserPassword(UserControllerContext context)
    {
        var password = context.Update.Message.Text;
        var client = await _authService.Login(new UserLoginModel()
        {
            Login = context.Session.LoginData.Login,
            Password = password
        });
        context.Session.LoginData = null;
        if (client is not null)
        {
            context.Session.ClientId = client.Id;
            context.Session.Controller = nameof(ClientDashboardController);
            context.Session.Action = nameof(ClientDashboardController.Index);
        
            await context.Forward(this._controllerManager);
            return;
        }
        else
            await context.SendBoldTextMessage("User not found❌");

        context.Session.Controller = null;
        context.Session.Action = null;

        await context.Forward(this._controllerManager);
    }

    public async Task RegistrationStart(UserControllerContext context)
    {
        context.Session.RegistrationModel = new UserRegistrationModel();
        await context.SendTextMessage("Enter your phone number :", context.RequesPhoheNumberReplyKeyboardMarkup());
        context.Session.Action = nameof(RegistrationPhoneNumber);
    }

    public async Task RegistrationPhoneNumber(UserControllerContext context)
    {
        var phoneNumber = context.Message?.Contact?.PhoneNumber;
        if (phoneNumber is null)
        {
            await context.SendErrorMessage("Wrong phone number!", 400);
            return;
        }
        
        if (!phoneNumber.StartsWith("+"))
            phoneNumber = "+" + phoneNumber;
        
        context.Session.RegistrationModel.PhoneNumber = phoneNumber;
        await context.SendTextMessage("Please Enter your password: ");
        context.Session.Action = nameof(RegistrationPassword);
    }

    public async Task RegistrationPassword(UserControllerContext context)
    {
        context.Session.RegistrationModel.Password = context.Update.Message.Text;
        context.Session.RegistrationModel.ChatId = context.Session.ChatId;

        await _authService.RegisterUser(context.Session.RegistrationModel);

        await context.SendBoldTextMessage("You Succesfully registired. Please re-sign in✅");

        context.Session.Controller = null;
        context.Session.Action = null;

        await context.Forward(this._controllerManager);
    }


    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(RegistrationStart):
            {
                await RegistrationStart(context);
                break;
            }
            case nameof(RegistrationPhoneNumber):
            {
                await RegistrationPhoneNumber(context);

                break;
            }
            case nameof(RegistrationPassword):
            {
                await RegistrationPassword(context);


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

    protected override Task HandleUpdate(UserControllerContext context)
    {
        return Task.CompletedTask;
    }
}