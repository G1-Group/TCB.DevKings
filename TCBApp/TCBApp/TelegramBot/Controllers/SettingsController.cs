using TCBApp.Services;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot.Types.Enums;

namespace TCBApp.TelegramBot.Controllers;

public class SettingsController : ControllerBase
{
    private readonly ClientDataService _clientDataService;
    private readonly BoardService _boardService;

    public SettingsController(ControllerManager.ControllerManager controllerManager,ClientDataService clientDataService,BoardService boardService) : base(controllerManager)
    {
        _clientDataService = clientDataService;
        _boardService = boardService;
    }

    public async Task Index(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Settings",context.SettingsStartReplyKeyboardMarkup());
    }

    public async Task DeleteBoards(UserControllerContext context)
    {
        // await _boardService.DeleteBoard()
    }
    public async Task DeleteAccount(UserControllerContext context)
    {
    }
    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await this.Index(context);
                break;
            case nameof(DeleteBoards):
                await this.DeleteBoards(context);
                break;
            case nameof(DeleteAccount):
                await this.DeleteAccount(context);
                break;
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (message.Type is MessageType.Text)
        {
            if (message.Text == "Back")
            {
                context.Session.Controller = nameof(BoardController);
                context.Session.Action = nameof(BoardController.Index);
                return;
            }
            context.Session.Action = message.Text switch
            {
                "Delete Boards" => nameof(this.DeleteBoards),
                "Delete Accaunt" => nameof(this.DeleteAccount),
                _ => context.Session.Action
            };
        }
    }
}