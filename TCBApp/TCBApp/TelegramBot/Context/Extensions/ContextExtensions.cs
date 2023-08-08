using System.Runtime.CompilerServices;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;

namespace TCBApp.TelegramBot.Extensions;

public static class ContextExtensions
{
    public static async Task Forward(this UserControllerContext context, ControllerManager.ControllerManager controllerManager)
    {
        ControllerBase baseController = controllerManager.GetControllerBySessionData(context.Session);
        await baseController.Handle(context);
    }
    
    public static async Task<Message> SendTextMessage(this UserControllerContext context, string text)
    {
        return await TelegramBot._client.SendTextMessageAsync(context.Update.Message!.Chat.Id, text);
    }
}