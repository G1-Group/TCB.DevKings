using Microsoft.EntityFrameworkCore;
using TCBApp.Services;
using TCBApp.Services.DataContexts;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Controllers;
using TCBApp.TelegramBot.Extensions;
using TCBApp.TelegramBot.Managers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot;

public class TelegramBot
{
    private readonly ClientDataService _clientDataService;
    private readonly UserDataService _userDataService;
    private readonly AuthService _authService;
    private readonly BoardService _boardService;
    private readonly BoardDataService _boardDataService;
    private readonly MessageDataService _messageDataSerice;
    private readonly ConversationDataService _conversationDataService;
    public static TelegramBotClient _client { get; set; }

    private DataContext DataContext { get; set; }
    private SessionManager SessionManager { get; set; }
    
    private ControllerManager.ControllerManager ControllerManager { get; set; }
    private List<Func<UserControllerContext, CancellationToken, Task>> updateHandlers { get; set; }


    public TelegramBot()
    {
        DataContext = new DataContext();
        _client = new TelegramBotClient(Settings.botToken);
        
        _userDataService = new UserDataService(DataContext);
        _clientDataService = new ClientDataService(DataContext);
        _boardDataService = new BoardDataService(DataContext);
        _messageDataSerice = new MessageDataService(DataContext);
        _conversationDataService = new ConversationDataService(DataContext);
        
        _authService = new AuthService(_userDataService, _clientDataService);
        _boardService = new BoardService(_boardDataService, _messageDataSerice);
        
        SessionManager = new SessionManager(_userDataService);
        
        ControllerManager =
            new ControllerManager.ControllerManager(
                _userDataService, 
                _clientDataService,
                _authService,
                _boardService,
                _messageDataSerice,
                _conversationDataService,
                SessionManager);

        updateHandlers = new List<Func<UserControllerContext, CancellationToken, Task>>();
    }

    public async Task Start()
    {
        //Session handler
        this.updateHandlers.Add(async (context, token) =>
        {
            long? chatId = context.GetChatIdFromUpdate();
            
            if (chatId is null)
                throw new Exception("Chat id not found to find session");

            var session = await SessionManager.GetSessionByChatId(chatId.Value);
            context.Session = session;
            context.TerminateSession = async () => await this.SessionManager.TerminateSession(context.Session);
        });
        //Log handler
        this.updateHandlers.Add(async (context, token) =>
        {
            Console.WriteLine("Log -> {0} | {1} | {2}", DateTime.Now, context.Session.ChatId,
                context.Update.Message?.Text ?? context.Update.Message?.Caption);
        });

        this.updateHandlers.Add(async (context, token) =>
        {
            if (context.Update.Type != UpdateType.ChosenInlineResult)
                return;
            
            var result = context.Update.ChosenInlineResult;

            string queryId = context.Session.InlineResultQueryId;
            context.Session.Controller = nameof(BoardController);
            context.Session.Action = nameof(BoardController.SendMessageToBoardStart);
        });
        
        this.updateHandlers.Add(async (context, token) =>
        {
            var signedUser = await _userDataService
                .GetAll()
                .FirstOrDefaultAsync(user =>
                    user.Signed
                    && user.TelegramClientId == context.GetChatIdFromUpdate());
            if (signedUser is not null)
            {
                context.Session.User = signedUser;
                context.Session.ClientId = signedUser.Client.Id;
            }
        });
        
        //Check for auth
        List<string> authRequiredControllers = new List<string>()
        {
            nameof(BoardController),
            nameof(ClientDashboardController),
            nameof(ConversationsController),
            nameof(ClientInfoController),
            nameof(SettingsController)
        };
        
        this.updateHandlers.Add(async (context, token) =>
        {
            if (context.Session is not null )
            {
                if (context.Session.ClientId is not null)
                {
                    string controller = context.Session.Controller ?? nameof(HomeController);
                    if (nameof(HomeController) == controller || controller == nameof(AuthController))
                    {
                        context.Session.Controller = nameof(ClientDashboardController);
                        context.Session.Action = nameof(ClientDashboardController.Index);
                    } 
                } else if (authRequiredControllers.Contains(context.Session.Controller))
                {
                    await context.SendErrorMessage("Unauthorized", 401);
                    context.Session.Controller = null;
                    context.Session.Action = null;
                };
            }
            
            
            
        });
        

        this.updateHandlers.Insert(this.updateHandlers.Count,
            async (context, token) => { await context.Forward(this.ControllerManager); });

        await StartReceiver();
    }

    private async Task StartReceiver()
    {
        var cancellationToken = new CancellationToken();
        var options = new ReceiverOptions();
        _client.StartReceiving(OnUpdate, ErrorMessage, options, CancellationToken.None);
        Console.WriteLine("{0} | Bot is starting...", DateTime.Now);
        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (sender, args) =>
        {
            cts.Cancel();
        };

        while (!cts.IsCancellationRequested)
        {
            
        }
    }


    private async Task OnUpdate(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        UserControllerContext context = new UserControllerContext()
        {
            Update = update
        };

        try
        {
                foreach (var updateHandler in this.updateHandlers)
                    await updateHandler(context, token);
        }
        catch (Exception e)
        {
            context.Reset();
            
            string errorMessage = ("Handler Error: " + e.Message 
                                                     + "\nInner exception message: " 
                                                     + e.InnerException?.Message
                                                // + "\nStack trace: " + e.StackTrace
                                                );
            Console.WriteLine(errorMessage + "\nStackTrace: " + e.StackTrace);
            await context.SendErrorMessage(errorMessage, 500);
        }
    }


    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }
}