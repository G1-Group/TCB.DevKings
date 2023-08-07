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

    private List<UpdateHandlerDelegate> updateHandlers { get; set; }


    private KeyboardButton KeyboardButton;
  
    public TelegramBot(SessionManager sessionManager,ControllerManager.ControllerManager manager)
    {
        ControllerManager = manager;
        SessionManager = sessionManager;
        updateHandlers = new List<UpdateHandlerDelegate>();
        
    
    }

    public async Task Start()
    {
        await StartReceiver();
    }

    private async Task StartReceiver()
    {
        var cancellationToken = new CancellationToken();
        var options = new ReceiverOptions();
        await _client.ReceiveAsync(OnMessage, ErrorMessage, options, cancellationToken);
        
    }

    private async Task OnMessage(ITelegramBotClient bot, Update update, CancellationToken token)
    {
      
         
            
            
      
        
    }


    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }


 
    
 
}