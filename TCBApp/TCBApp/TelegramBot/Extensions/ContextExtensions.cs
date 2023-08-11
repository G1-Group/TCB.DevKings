using System.Runtime.CompilerServices;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot.Extensions;

public static class ContextExtensions
{
    public static async Task Forward(this UserControllerContext context, ControllerManager.ControllerManager controllerManager)
    {
        ControllerBase baseController = controllerManager.GetControllerBySessionData(context.Session);
        await baseController.Handle(context);
    }
    
    public static async Task ForwardToIndex(this UserControllerContext context, ControllerManager.ControllerManager controllerManager)
    {
        ControllerBase baseController = controllerManager.GetControllerBySessionData(context.Session);
        await baseController.Handle(context);
    }
    
    public static async Task<Message> SendTextMessage(this UserControllerContext context, string text, IReplyMarkup? replyMarkup = null, ParseMode? parseMode = null)
    {
        return await TelegramBot._client.SendTextMessageAsync(context.Update.Message!.Chat.Id, text, replyMarkup: replyMarkup, parseMode: parseMode);
    }
    
    public static async Task<Message> SendErrorMessage(this UserControllerContext context, string text = null, int code = 404)
    {
        string codeText = code switch
        {
            400 => "4️⃣0️⃣0️⃣",
            401 => "4️⃣0️⃣1️⃣",
            500 => "5️⃣0️⃣0️⃣",
            _ => "4️⃣0️⃣4️⃣"
        };
        return await context.SendTextMessage($"<b>{codeText} {text ?? "Not found!"}</b>", parseMode: ParseMode.Html);
    }
    
    public static async Task<Message> SendBoldTextMessage(this UserControllerContext context, string text, IReplyMarkup? replyMarkup = null, ParseMode? parseMode = null)
    {
        return await context.SendTextMessage($"<b>{text}</b>", parseMode: parseMode ?? ParseMode.Html, replyMarkup: replyMarkup);
    }
    
}