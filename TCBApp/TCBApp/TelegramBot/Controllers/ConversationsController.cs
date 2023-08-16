using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Extensions;
using TCBApp.TelegramBot.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using MessageType = Telegram.Bot.Types.Enums.MessageType;

namespace TCBApp.TelegramBot.Controllers;

public class ConversationsController : ControllerBase
{
    private readonly ConversationDataService _conversationDataService;
    private readonly ISessionManager<Session> _sessionManager;
    private readonly Queue<long> waitingClients = new Queue<long>();

    public ConversationsController(ControllerManager.ControllerManager controllerManager,
        ConversationDataService conversationDataService, ISessionManager<Session> sessionManager) : base(controllerManager)
    {
        _conversationDataService = conversationDataService;
        _sessionManager = sessionManager;
    }

    public async Task Index(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Welcome to conversation !!!",
            context.ConversationStartReplyKeyboardMarkup());
    }

    public async Task StartConversation(UserControllerContext context)
    {
        var alreadyExists = _conversationDataService
            .GetAll()
            .Any(x =>
                (x.ToId == context.Session.ClientId
                 ||
                 x.FromId == context.Session.ClientId
                 )
                && x.State == ChatState.Opened);
        
        var clientId = waitingClients
            .FirstOrDefault(x => x == context.Session.ClientId);

        if (clientId != 0 || alreadyExists)
        {
            await context.SendErrorMessage("Already queued!", 400);
            return;
        }            
            
        if (waitingClients.Count == 0)
        {
            waitingClients.Enqueue(context.Session.ClientId.Value);
            await context.SendBoldTextMessage("Waiting...");
        }
        else
        {
            
            var fromId = waitingClients.Dequeue();

            var chat = new ChatModel()
            {
                FromId = fromId,
                ToId = context.Session.ClientId.Value,
                State = ChatState.Opened,
                CreatedDate = DateTime.Now,
            };

            var insertedChat = await _conversationDataService
                .AddAsync(chat);

            context.Session.ActiveConversationId = insertedChat.Id;
            context.Session.Action = nameof(WriteToAnonymRoom);
            
            var fromSession = await _sessionManager.GetSessionByClientId(fromId);
            fromSession.ActiveConversationId = insertedChat.Id;
            fromSession.Action = nameof(WriteToAnonymRoom);

            string message = "<b>New conversation started!</b>";
            await _botClient.SendTextMessageAsync(
                fromSession.ChatId,
                message, 
                parseMode: ParseMode.Html,
                replyMarkup: context.StopConversationReplyKeyboardMarkup());

            await _botClient.SendTextMessageAsync(
                context.Session.ChatId,
                message,
                parseMode: ParseMode.Html,
                replyMarkup: context.StopConversationReplyKeyboardMarkup());
        }
    }

    public async Task WriteToAnonymRoom(UserControllerContext context)
    {

        if (context.Session.ActiveConversationId is null)
            throw new Exception("Conversation not found");
        
        var conversation = await _conversationDataService
            .GetByIdAsync(context.Session.ActiveConversationId.Value);

        var to =
            conversation.From.Id == context.Session.ClientId
                ? conversation.To
                : conversation.From;

        var from =
            conversation.From.Id == context.Session.ClientId
                ? conversation.From
                : conversation.To;
        
        await _botClient.CopyMessageAsync(to.User.TelegramClientId, from.User.TelegramClientId, message.MessageId);
    }

    public async Task StopConversation(UserControllerContext context)
    {
        if (context.Session.ActiveConversationId is null)
            throw new Exception("Conversation not found");

        var conversation = await _conversationDataService
            .GetByIdAsync(context.Session.ActiveConversationId.Value);

        conversation.State = ChatState.Closed;

        await _conversationDataService.UpdateAsync(conversation);

        var fromSession = await _sessionManager.GetSessionByClientId(conversation.FromId);
        var toSession = await _sessionManager.GetSessionByClientId(conversation.ToId);

        fromSession.Action = nameof(ConversationsController.Index);
        toSession.Action = nameof(ConversationsController.Index);

        fromSession.ActiveConversationId = null;
        toSession.ActiveConversationId = null;
        
        var message = "<b>Conversation finished. Press back to continue...</b>";
        await _botClient.SendTextMessageAsync(
            fromSession.ChatId,
            message, 
            parseMode: ParseMode.Html,
            replyMarkup: context.Back());

        await _botClient.SendTextMessageAsync(
            toSession.ChatId,
            message,
            parseMode: ParseMode.Html,
            replyMarkup: context.Back());
    }

    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await this.Index(context);
                break;
            case nameof(StartConversation):
                await this.StartConversation(context);
                break;
            case nameof(WriteToAnonymRoom):
                await this.WriteToAnonymRoom(context);
                break;
            case nameof(StopConversation):
                await this.StopConversation(context);
                break;
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (message.Type is MessageType.Text)
        {
            if (message.Text == "back")
            {
                context.Session.Controller = nameof(ClientDashboardController);
                context.Session.Action = nameof(ClientDashboardController.Index);
                return;
            }
            
            context.Session.Action = message.Text switch
            {
                "start" => nameof(this.StartConversation),
                "stop" => nameof(this.StopConversation),
                _ => context.Session.Action
            };
        }
    }
}