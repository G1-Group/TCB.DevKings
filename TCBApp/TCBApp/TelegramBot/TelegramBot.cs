using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot;

public class TelegramBot
{
    private TelegramBotClient _client { get; set; }

  

    public delegate Task UpdateHandlerDelegate(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);

    private List<UpdateHandlerDelegate> updateHandlers { get; set; }


    private KeyboardButton KeyboardButton;
    public TelegramBot()
    {
       updateHandlers = new List<UpdateHandlerDelegate>();
        
        _client = new TelegramBotClient("5767267731:AAEVGTs0gB_PmSOxRHpbA7g8WlWdZ4vu");
        
       
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
        if (update.Message is Message message)
        {
            
            
            
      
        }
    }



    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }
  
    
   
 
}