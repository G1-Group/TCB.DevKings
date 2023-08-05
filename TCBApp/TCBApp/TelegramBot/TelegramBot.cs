using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot;

public class TelegramBot
{
    private TelegramBotClient _client { get; set; }
    private ReplyKeyboardMarkup _keyboard;


    private KeyboardButton KeyboardButton;
    public TelegramBot()
    {
        
        _client = new TelegramBotClient("5767267731:AAEVGTs0gB_PmSOxRHpbA7g8WlWdZ4vu");
        
        _keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                 new KeyboardButton("Login")
            },
            new[]
            {
                new KeyboardButton("Sign up")
            }
        });
        _keyboard.ResizeKeyboard = true;
        
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
            
            if (message.Text == "/start")
                await SendMessage(message, "Assalomu aleykum.",_keyboard);
            
      
        }
    }

    private async Task SendMessage(Message message, string text,ReplyKeyboardMarkup markup)
    {
        await _client.SendTextMessageAsync(message.Chat.Id, text,replyMarkup:markup);
    }


    private async Task ErrorMessage(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        // Handle any errors that occur during message processing here.
    }
  

}