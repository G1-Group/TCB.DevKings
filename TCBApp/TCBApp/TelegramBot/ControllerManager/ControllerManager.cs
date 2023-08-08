using TCBApp.Models;
using TCBApp.Services;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot;

namespace TCBApp.TelegramBot.ControllerManager;

public class ControllerManager
{
    private AuthController _authController;
    private HomeController _homeController;
    // private BoardController BoardController;
    // private ConversationController ConversationController;
    public ControllerManager(UserDataService dataService, ClientDataService clientDataService, AuthService authService)
    {
        this._homeController = new HomeController(this);
        this._authController = new AuthController(authService, this);
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
        }
        
        return this.DefaultController;
    }

    public ControllerBase DefaultController => this._homeController;
}