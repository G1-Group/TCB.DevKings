using TCBApp.Interface;
using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Services;

public class BoardService:IBoardInterface
{
    private DataProvider dataProvicer { get; }

    public BoardService(DataProvider _dataProvicer)
    {
        _dataProvicer = dataProvicer;
    }
    public BoardModel CreateBoard(BoardModel boardModel)
    {
        return dataProvicer.CreateBoard(boardModel);
    }

    public BoardModel StopBoard(long boardId)
    {
        return dataProvicer.StopBoard(boardId);
    }

    public BoardModel DeleteBoard(BoardModel boardModel)
    {
        return dataProvicer.DeleteBoard(boardModel);
    }

    public BoardModel UpdateBoard(BoardModel boardModel)
    {
        return dataProvicer.UpdateBoard(boardModel);
    }

    public BoardModel GetBoard(long boardId)
    {
        return dataProvicer.GetBoard(boardId);
    }

    public List<BoardModel> GetAllBoards()
    {
        return dataProvicer.GetAllBoards();
    }
}
   
