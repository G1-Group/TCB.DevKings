using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Services;
using TCBApp.Services.DataContexts;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Controllers;
using TCBApp.TelegramBot.Managers;
using Telegram.Bot;

namespace TCBApp.TelegramBot.ControllerManager;

public class ControllerManager
{
    private readonly ClientInfoController _clientInfoController;
    private readonly AuthController _authController;
    private readonly HomeController _homeController;
    private readonly ClientDashboardController _clientDashboardController;
    private readonly BoardController _boardController;
    private readonly MessageDataService MessageDataService;
    private readonly SessionManager _sessionManager;
    private MessageService _messageService;
    private DataContext DataContext;
    private readonly ConversationsController _conversationsController;

    private readonly SettingsController _settingsController;

    // private BoardController BoardController;
    // private ConversationController ConversationController;
    public ControllerManager(
        UserDataService dataService, 
        ClientDataService clientDataService, 
        AuthService authService, 
        BoardService boardService,
        MessageDataService messageDataService,
        ConversationDataService conversationDataService,
        SessionManager sessionManager)
    {
        MessageDataService = messageDataService;
        _sessionManager = sessionManager;
        this._homeController = new HomeController(this);
        this._authController = new AuthController(authService, this);
        this._clientDashboardController = new ClientDashboardController(this, clientDataService);
        _messageService = new MessageService(MessageDataService);
        this._boardController = new BoardController(this, boardService,_messageService);
        DataContext = new DataContext();
        _conversationsController = new ConversationsController(this,conversationDataService, _sessionManager);
        this._homeController = new HomeController(this);
        this._authController = new AuthController(authService, this);
        this._clientDashboardController = new ClientDashboardController(this, clientDataService);
        this._settingsController = new SettingsController(this,clientDataService, boardService);
        _clientInfoController = new ClientInfoController(this,clientDataService);


        // this._authController = new AuthController(botClient, new AuthService(dataService));
        // this.BoardController = new BoardController(botClient);
        // this.ConversationController = new ConversationController(botClient);
    }

    public ControllerBase GetControllerBySessionData(Session session)
    {
        switch (session.Controller)
        {
            case nameof(HomeController):
                return this._homeController;
            case nameof(AuthController):
                return this._authController;
            case nameof(ClientDashboardController):
                return this._clientDashboardController;
            case nameof(BoardController):
                return this._boardController;
            case nameof(SettingsController):
                return this._settingsController;
            case nameof(ClientInfoController):
                return this._clientInfoController;
            case nameof(ConversationsController):
                return this._conversationsController;
        }
        
        return this.DefaultController;
    }

    public ControllerBase DefaultController => this._homeController;
}