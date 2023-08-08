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


   
  
    public TelegramBot(SessionManager sessionManager,ControllerManager.ControllerManager manager)
    {
        ControllerManager = manager;
        SessionManager = sessionManager;
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
        await _client.ReceiveAsync(OnUpdate, ErrorMessage, options, cancellationToken);
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
            Console.WriteLine("Handler Error: " + e.Message);
        }
        
    }


    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }
 
}