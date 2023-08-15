using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using MessageType = Telegram.Bot.Types.Enums.MessageType;

namespace TCBApp.TelegramBot.Controllers;

public class ConversationsController : ControllerBase
{
    private readonly ConversationDataService _conversationDataService;
    private readonly Queue<UserControllerContext> waitingClients = new Queue<UserControllerContext>();

    public ConversationsController(ControllerManager.ControllerManager controllerManager,
        ConversationDataService conversationDataService) : base(controllerManager)
    {
        _conversationDataService = conversationDataService;
    }

    public async Task Index(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Welcome to conversation !!!",
            context.ConversationStartReplyKeyboardMarkup());
    }

    public async Task StartConversation(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Waiting...",
            new ReplyKeyboardMarkup(new KeyboardButton(
                "Stop conversation"))
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            });
        context.Session.Action = "WriteToAnonymRoom";
        if (waitingClients.Count % 2 == 0)
        {
            waitingClients.Enqueue(context);
        }
        else
        {
            var clientFirst = waitingClients.Dequeue();
            var clientSecond = context;
            clientFirst.Session.AnonymTelegramChatIdFirst = clientFirst.Session.ChatId;
            clientFirst.Session.AnonymTelegramChatIdSecond = context.Session.ChatId;
            clientSecond.Session.AnonymTelegramChatIdFirst = clientSecond.Session.ChatId;
            clientSecond.Session.AnonymTelegramChatIdSecond = clientFirst.Session.ChatId;
            if (clientFirst.Session.ClientId != clientSecond.Session.ClientId)
            {
                ChatModel newChatModel = new ChatModel
                {
                    CreatedDate = DateTime.Now,
                    FromId = clientFirst.Session.ClientId!.Value,
                    ToId = clientSecond.Session.ClientId!.Value,
                    State = ChatState.Opened,
                };
                newChatModel = await _conversationDataService.AddAsync(newChatModel);

                await clientFirst.SendBoldTextMessage("Starting Conversation, please write !");
                await clientSecond.SendBoldTextMessage("Starting Conversation, please write !");
            }
        }
    }

    public async Task WriteToAnonymRoom(UserControllerContext context)
    {
        if (context.Session.AnonymTelegramChatIdFirst == context.Session.ChatId)
        {
            await _botClient.SendTextMessageAsync(context.Session.AnonymTelegramChatIdSecond, message.Text);
        }
        else if (context.Session.AnonymTelegramChatIdSecond == context.Session.ChatId)
        {
            await _botClient.SendTextMessageAsync(context.Session.AnonymTelegramChatIdFirst, message.Text);
        }

        if (message.Text == "Stop conversation")
        {
            context.Session.AnonymTelegramChatIdSecond = null;
            context.Session.AnonymTelegramChatIdFirst = null;
            context.Session.Action = nameof(Index);
        }
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
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (message.Type is MessageType.Text)
        {
            if (message.Text == "Back")
            {
                context.Session.Controller = nameof(ClientDashboardController);
                context.Session.Action = nameof(ClientDashboardController.Index);
                return;
            }
            
            if (message.Text == "Stop conversation")
            {
                context.Session.Controller = nameof(ClientDashboardController);
                context.Session.Action = nameof(ClientDashboardController.Index);
                return;
            }

            context.Session.Action = message.Text switch
            {
                "Start Conversation" => nameof(this.StartConversation),
                _ => context.Session.Action
            };
        }
    }
}