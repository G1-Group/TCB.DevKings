using TCBApp.Services;
using TCBApp.TelegramBot.Managers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
namespace TCBApp.TelegramBot;

public class TelegramBot
{
    private TelegramBotClient _client { get; set; }

    private SessionManager SessionManager { get; set; }
    private ControllerManager.ControllerManager ControllerManager { get; set; } 

    public delegate Task UpdateHandlerDelegate(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);

    private List<Func<UserControllerContext, CancellationToken, Task>> updateHandlers { get; set; }


    private UserDataService _dataService = new UserDataService(DBConnection.connection);
  
    public TelegramBot()
    {
        _client = new TelegramBotClient("5767267731:AAEVGTs0gB_PmSOxRHpbA7g8WlWdZ4vu_Ok");
        ControllerManager = new ControllerManager.ControllerManager(_client,_dataService,new ClientDataService(DBConnection.connection));
        SessionManager = new SessionManager(_dataService);
        updateHandlers = new List<Func<UserControllerContext, CancellationToken, Task>>();
    }

    public async Task Start()
    {
        //Session handler
        this.updateHandlers.Add(async (context, token) =>
        {
            if (context.Update?.Message?.Chat.Id is null)
                throw new Exception("Chat id not found to find session");
            
            var session = await SessionManager.GetSessionByChatId(context.Update.Message.Chat.Id);
            context.Session = session;
        });
        
        //Log handler
        this.updateHandlers.Add(async (context, token) =>
        {
            Console.WriteLine("Log -> {0} | {1} | {2}", DateTime.Now, context.Session.ChatId, context.Update.Message?.Text ?? context.Update.Message?.Caption);
        });
        
        await StartReceiver();
    }

    private async Task StartReceiver()
    {
        var cancellationToken = new CancellationToken();
        var options = new ReceiverOptions();
         _client.ReceiveAsync(OnUpdate, ErrorMessage, options, cancellationToken).Wait();
    }

    private async Task OnUpdate(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        UserControllerContext context = new UserControllerContext()
        {
            Update = update
        };
        Console.WriteLine(update.Message.Chat.Id+update.Message.Text);
        try
        {
            foreach (var updateHandler in this.updateHandlers)
                await updateHandler(context, token);
        }
        catch (Exception e)
        {
            Console.WriteLine("Handler Error: " + e.Message);
        }
        
    }


    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }
 
}