using TCBApp.Models;
using TCBApp.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Message = TCBApp.Models.Message;

namespace TCBApp.TelegramBot.Controllers;

public class BoardController:ControllerBase
{
    private BoardService boardService;
    public BoardController(ITelegramBotClient botClient) : base(botClient)
    {
    }
    
    public override void HandleAction(ControllerContext context)
    {
        if (context.Session.Action == nameof(GetBoardMessages))
            this.GetBoardMessages(context);
        
        else if(context.Session.Action == nameof(CreateBoard))
            this.CreateBoard(context);
        
        else if (context.Session.Action == nameof(DeleteBoard))
            this.DeleteBoard(context);
        
        else if (context.Session.Action == nameof(UpdateBoard))
            this.UpdateBoard(context);

        else if (context.Session.Action == nameof(GetBoard))
            this.GetBoard(context);
        
        else if (context.Session.Action == nameof(GetAllBoards))
            this.GetAllBoards(context);
        
        else return;
    }

    public async Task GetBoardMessages(ControllerContext context)
    {
        var MessageList = boardService.GetBoardMessages(context.Update.Message.MessageId).Result;
        foreach (Message message in MessageList)
        {
            _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, context.Update.Message.Text).Wait();
        }
    }

    public async  Task CreateBoard(ControllerContext context)
    {
        boardService.CreateBoard(new BoardModel()
        {
            OwnerId = context.Session.User.UserId,
            NickName = context.Update.Message.Text
        });
    }

    public async Task UpdateBoard(ControllerContext context)
    {
        var Boards = boardService.GetBoardFromUserId(context.Session.User.UserId);
        var nickName = context.Update.Message.Text;
        var board = boardService.FindBoardByNickName(nickName);
        if (board is not null)
        {
            _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Create new NickName ").Wait();
        }
    }
    
    public async Task DeleteBoard(ControllerContext context)
    {
        var Boards = boardService.GetBoardFromUserId(context.Session.User.UserId);
        var nickName = context.Update.Message.Text;
        var board = boardService.FindBoardByNickName(nickName);
        boardService.DeleteBoard(board.BoardId);
    }

    public async Task GetBoard(ControllerContext context)
    {
        boardService.FindBoardByNickName(context.Update.Message.Text);
    }

    public async Task<List<BoardModel>> GetAllBoards(ControllerContext context)
    {
        var Boards = boardService.GetBoardFromUserId(context.Session.User.UserId);
        return Boards;
    }
    
}