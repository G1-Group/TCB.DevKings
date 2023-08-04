using TCBApp.Interface;
using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Services;

public class BoardService:IBoardInterface
{
    private DataProvider dataProvider { get; }

    public BoardService(DataProvider _dataProvicer)
    {
        _dataProvicer = dataProvider;
    }
    public BoardModel CreateBoard(BoardModel boardModel)
    {
        return dataProvider.CreateBoard(boardModel);
    }

    public BoardModel StopBoard(long boardId)
    {
        return dataProvider.StopBoard(boardId);
    }

    public BoardModel DeleteBoard(BoardModel boardModel)
    {
        return dataProvider.DeleteBoard(boardModel);
    }

    public BoardModel UpdateBoard(BoardModel boardModel)
    {
        return dataProvider.UpdateBoard(boardModel);
    }

    public BoardModel GetBoard(long boardId)
    {
        return dataProvider.GetBoard(boardId);
    }

    public List<BoardModel> GetAllBoards()
    {
        return dataProvider.GetAllBoards();
    }
}
   
