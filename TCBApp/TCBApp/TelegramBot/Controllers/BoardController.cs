using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services;
using TCBApp.Services.DataService;
using TCBApp.TelegramBot.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using MessageType = Telegram.Bot.Types.Enums.MessageType;

namespace TCBApp.TelegramBot.Controllers;

public class BoardController : ControllerBase
{
    private readonly BoardService _boardService;
    private readonly BoardDataService _boardDataService;
    private MessageService _messageService;

    public BoardController(ControllerManager.ControllerManager controllerManager, BoardService boardService,
        MessageService messageService) : base(controllerManager)
    {
        _messageService = messageService;
        _boardService = boardService;
        _boardDataService = boardService._boardDataService;
    }

    public async Task FindBoardStart(UserControllerContext context)
    {
        // context.Session.Action = nameof(FindBoardNickName);
        await context.SendTextMessage("Enter board nickname: ", replyMarkup: context.FindBoardsReplyKeyboardMarkup());
    }

    public async Task FindBoardNickName(UserControllerContext context, long boardId)
    {
        var result = await _boardDataService.GetByIdAsync(boardId);
        if (result is not null)
        {
            await context.SendTextMessage("Board topildi.", context.FindBoardNickNameReplyKeyboardMarkup());
            context.Session.BoardData = new BoardSessionModel();
            context.Session.BoardData.NewBoardNickName = result.NickName;
            context.Session.BoardData.BoardId = result.Id;
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

    public async Task SendMessageToBoardStart(UserControllerContext context)
    {
        await context.SendTextMessage("Write message✍️: ", replyMarkup: new ReplyKeyboardRemove());
        context.Session.Action = nameof(SendMessageToBoard);
    }

    public async Task SendMessageToBoard(UserControllerContext context)
    {
        Console.WriteLine(nameof(SendMessageToBoard));
        context.Session.Action = nameof(MyBoards);
        var res = await _messageService.SendMessageToBoard(new SendMessageToBoardViewModel(
            BoardId: context.Session.BoardData.BoardId,
            FromId: context.Session.ClientId.Value,
            Content: context.Update.Message
        ));

        if (res is Message)
            await context.SendTextMessage("Xabar yuborildi✅", context.FindBoardNickNameReplyKeyboardMarkup());
        else
            await context.SendTextMessage("Xabar yuborilmadi.Xatolik yuz berdi❌",
                context.FindBoardNickNameReplyKeyboardMarkup());
    }


    public async Task FindBoardMessages(UserControllerContext context)
    {
    }


    public async Task MyBoards(UserControllerContext context)
    {
        var allBoardsQuery = _boardDataService.GetAll()
            .Where(x =>
                x.OwnerId == context.Session.ClientId!.Value);

        if (!(await allBoardsQuery.AnyAsync()))
        {
            context.Session.Action = nameof(HandleUpdate);
            await context.SendBoldTextMessage("Sizda hali boardlar mavjud emas🤷‍",
                replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
            return;
        }

        var allBoards = await allBoardsQuery.ToListAsync();
        string message = $"All boards count: {allBoards.Count};\n";

     

        await context.SendTextMessage($"<code>Your Boards </code>", parseMode: ParseMode.Html,
            replyMarkup: context.MyBoardsKeyboardMarkup(allBoards));
    }

    public async Task Index(UserControllerContext context)
    {
        await context.SendBoldTextMessage("Your boards", replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
    }

    public async Task CreateBoardStart(UserControllerContext context)
    {
        context.Session.BoardData = new BoardSessionModel();
        context.Session.Action = nameof(CreateBoardNickname);
        await context.SendTextMessage("Enter nickname for new board✍️: ", replyMarkup: new ReplyKeyboardRemove());
    }


    public async Task GetBoardMessagesStart(UserControllerContext context)
    {
        await context.SendTextMessage("Board nicknameni kiriting: ");
        context.Session.Action = nameof(GetBoardMessages);
    }

    public async Task GetBoardMessages(UserControllerContext context)
    {
        var res = await _messageService.GetBoardMessages(context.Session.BoardData.BoardId);
        if (res.Count > 0)
        {
            foreach (Message m in res)
            {
                var tgMessage = m.Content;
                if (tgMessage.Type == MessageType.Text)
                    await context.SendTextMessage($"{tgMessage.Text}");
                else
                    await context.SendBoldTextMessage($"No content!");
            }
        }
        else
        {
            await context.SendTextMessage("Sizga habarlar mavjud emas");
        }

        context.Session.Controller = nameof(BoardController);
        context.Session.Action = nameof(MyBoards);
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

        context.Session.BoardData.NewBoardNickName = context.Session.BoardData.NewBoardNickName;
        context.Session.Action = nameof(Index);
        await context.SendBoldTextMessage("Board successfully created✅",
            replyMarkup: context.MakeBoardsReplyKeyboardMarkup());
    }


    public async Task HandleCallBackQuery(UserControllerContext context)
    {
        var query = context.Update.CallbackQuery;
        
        if (!long.TryParse(query.Data, out long boardId))
        {
            throw new Exception("Board not found");
        }

        if (query.Message is null)
        {
            context.Session.BoardData = new BoardSessionModel()
            {
                BoardId = boardId
            };
            context.Session.Action = nameof(SendMessageToBoardStart);
            await this.FindBoardNickName(context, boardId);
            return;
        }

        var board = await _boardDataService.GetByIdAsync(boardId);
        var message = board?.Messages?.LastOrDefault();
        
        await _botClient
            .EditMessageTextAsync(
                context.GetChatIdFromUpdate()!,
                query.Message!.MessageId,
                (message?.Content?.Text ?? "No content"),
                ParseMode.Html
            );
    }

    public async Task HandleInlineQuery(UserControllerContext context)
    {
        var query = context.Update.InlineQuery;
        var searchTerm = query.Query;
        
        var result = await _boardDataService
            .GetAll()
            .Where(x => EF.Functions.ILike(x.NickName, $"%{searchTerm}%"))
            .Take(45)
            .Select(x =>
                new InlineQueryResultArticle(x.Id.ToString(), x.NickName,
                    new InputTextMessageContent("Click board to open"))
                {
                    Description = (x.Owner.Nickname.Length == 0 || x.Owner.Nickname == null ? 
                        x.Owner.User.PhoneNumber 
                        : x.Owner.Nickname),
                    ReplyMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton(x.NickName){CallbackData = x.Id.ToString()}),
                })
            .ToListAsync();

        context.Session.InlineResultQueryId = query.Id;
        
        await _botClient
            .AnswerInlineQueryAsync(
                query.Id,
                result,
                1,
                button: new InlineQueryResultsButton("button")
                {
                    StartParameter = "selected_action"
                }
            );


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
            case nameof(SendMessageToBoard):
                await this.SendMessageToBoard(context);
                break;
            case nameof(GetBoardMessages):
                await this.GetBoardMessages(context);
                break;
            case nameof(SendMessageToBoardStart):
                await this.SendMessageToBoardStart(context);
                break;
            case nameof(GetBoardMessagesStart):
                if (context.Session.BoardData.NewBoardNickName is null)
                    await this.GetBoardMessagesStart(context);
                else
                {
                    await this.GetBoardMessages(context);
                }
                break;
            case nameof(HandleCallBackQuery):
                await HandleCallBackQuery(context);
                break;
            case nameof(HandleInlineQuery):
                await HandleInlineQuery(context);
                break;
        }
    }

    protected override async Task HandleUpdate(UserControllerContext context)
    {
        if (context.Update.Type == UpdateType.Message && message.Type is MessageType.Text)
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
                "Send message" => nameof(this.SendMessageToBoardStart),
                "Messages" => nameof(this.GetBoardMessagesStart),
                _ => context.Session.Action
            };
        }

        if (context.Update.Type == UpdateType.CallbackQuery)
        {
            context.Session.Action = nameof(HandleCallBackQuery);
        }

        if (context.Update.Type == UpdateType.InlineQuery)
        {
            context.Session.Action = nameof(HandleInlineQuery);
        }
        
    }
}