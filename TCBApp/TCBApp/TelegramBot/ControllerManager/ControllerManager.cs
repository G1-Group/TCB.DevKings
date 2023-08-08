using TCBApp.Models;
using TCBApp.Services;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot;

namespace TCBApp.TelegramBot.ControllerManager;

public class ControllerManager
{
    
    private readonly ITelegramBotClient _botClient;
    private AuthController _authController;
    private HomeController _homeController;
    private BoardController BoardController;
    private ConversationController ConversationController;
    public ControllerManager(ITelegramBotClient botClient,UserDataService dataService,ClientDataService clientDataService)
    {
        _botClient = botClient;
        this._homeController = new HomeController(botClient);
        this._authController = new AuthController(botClient, new AuthService(dataService));
        this.BoardController = new BoardController(botClient);
        this.ConversationController = new ConversationController(botClient);
    }

    public ControllerBase GetControllerBySessionData(Session session)
    {
        if (session.Controller == nameof(AuthController))
            return this._authController;
        else if (session.Controller==nameof(BoardController))
            return BoardController;
        else if(session.Controller==nameof(ConversationController))
        {
            return ConversationController;
        }
            return this.DefaultController;
    }

    public ControllerBase DefaultController => this._homeController;
}