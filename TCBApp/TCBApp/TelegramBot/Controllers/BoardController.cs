using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using MessageType = Telegram.Bot.Types.Enums.MessageType;

namespace TCBApp.TelegramBot.Controllers;

public class BoardController:ControllerBase
{
    private readonly BoardService _boardService;
    private readonly BoardDataService _boardDataService;

    public BoardController(ControllerManager.ControllerManager controllerManager, BoardService boardService) : base(controllerManager)
    {
        _boardService = boardService;
        _boardDataService = boardService.boardDataService;
    }

    public async Task FindBoardStart(UserControllerContext context)
    {
        context.Session.Action = nameof(FindBoardNickName);
        await context.SendTextMessage("Enter nickname for new board: ", replyMarkup: new ReplyKeyboardRemove());
    }
    public async Task FindBoardNickName(UserControllerContext context)
    {
        var result = await _boardService.FindBoardByNickName(message.Text);
        if (result is not null)
        {
            await context.SendTextMessage("Board topildi.",context.FindBoardNickNameReplyKeyboardMarkup());
        }
        else
        {
            context.Session.Action = nameof(Index);
            await context.SendTextMessage("Not found 🤷‍♂️",
                new ReplyKeyboardMarkup(new KeyboardButton("Back"))
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            });
        }
    }

    public async Task FindBoardNickNameSendMessage(UserControllerContext context)
    {
    }

    public async Task FindBoardMessages(UserControllerContext context)
    {
        
    }

    public async Task MyBoards(UserControllerContext context)
    {
        var allBoards = await _boardDataService.GetAllByOwnerId(context.Session.ClientId.Value);

        if (allBoards.Count == 0)
        {
            context.Session.Action = nameof(Index);
            await context.SendBoldTextMessage("Sizda hali boardlar mavjud emas!", replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
            return;
        }
        
        string message = $"All boards count: {allBoards.Count};\n";
        
        message += string.Join(
            "\n",
            allBoards.Select(board => $"{board.BoardId} | {board.NickName} | {board.BoardStatus};")
        );

        await context.SendTextMessage($"<code>{message}</code>", parseMode: ParseMode.Html, replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
        
    }
    
    public async Task Index(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Your boards", replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
    }

    public async Task CreateBoardStart(UserControllerContext context)
    {
        context.Session.BoardData = new BoardSessionModel();
        context.Session.Action = nameof(CreateBoardNickname);
        await context.SendTextMessage("Enter nickname for new board: ", replyMarkup: new ReplyKeyboardRemove());
    }

    public async Task CreateBoardNickname(UserControllerContext context)
    {
        context.Session.BoardData.NewBoardNickName = message.Text;
        if (string.IsNullOrEmpty(context.Session.BoardData.NewBoardNickName))
        {
            await context.SendErrorMessage("Boardning nomi bo'sh bo'lmaydi", code: 400);
            return;
        }
        await _boardService.CreateBoard(new BoardModel()
        {
            BoardStatus = BoardStatus.New,
            NickName = context.Session.BoardData.NewBoardNickName,
            OwnerId = context.Session.ClientId!.Value
        });

        context.Session.BoardData = null;
        context.Session.Action = nameof(Index);
        await context.SendBoldTextMessage("Board successfully created✅", replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
    }

    protected override async Task HandleAction(UserControllerContext context)
    {
        switch (context.Session.Action)
        {
            case nameof(Index):
                await this.Index(context);
                break;
            case nameof(MyBoards):
                await this.MyBoards(context);
                break;
            case nameof(CreateBoardStart):
                await this.CreateBoardStart(context);
                break;
            case nameof(CreateBoardNickname):
                await this.CreateBoardNickname(context);
                break;
            case nameof(FindBoardStart):
                await this.FindBoardStart(context);
                break;
            case nameof(FindBoardNickName):
                await this.FindBoardNickName(context);
                break;
        }
    }
    
    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (message.Type is MessageType.Text)
        {
            if (message.Text == "Back")
            {
                context.Session.Controller = nameof(ClientDashboardController);
                context.Session.Action = nameof(ClientDashboardController.Index);
                return;
            }
            context.Session.Action = message.Text switch
            {
                "My boards" => nameof(this.MyBoards),
                "Create" => nameof(this.CreateBoardStart),
                "Find board" => nameof(this.FindBoardStart),
                _ => context.Session.Action
            };
        }
    }
}