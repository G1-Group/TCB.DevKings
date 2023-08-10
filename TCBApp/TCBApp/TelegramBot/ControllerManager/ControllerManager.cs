using TCBApp.Models;
using TCBApp.Services;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot;

namespace TCBApp.TelegramBot.ControllerManager;

public class ControllerManager
{
    private readonly AuthController _authController;
    private readonly HomeController _homeController;
    private readonly ClientDashboardController _clientDashboardController;
    private readonly BoardController _boardController;
    // private BoardController BoardController;
    // private ConversationController ConversationController;
    public ControllerManager(UserDataService dataService, ClientDataService clientDataService, AuthService authService, BoardService boardService)
    {
        this._homeController = new HomeController(this);
        this._authController = new AuthController(authService, this);
        this._clientDashboardController = new ClientDashboardController(this, clientDataService);
        this._boardController = new BoardController(this, boardService);

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
        }
        
        return this.DefaultController;
    }

    public ControllerBase DefaultController => this._homeController;
}