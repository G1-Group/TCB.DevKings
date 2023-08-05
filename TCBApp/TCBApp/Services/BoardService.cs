using TCBApp.Interface;
using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Services;

public class BoardService:IBoardInterface
{
    private BoardDataService boardDataService;
    public int CreateBoard(BoardModel boardModel)
    {
        return boardDataService.Insert(boardModel).Result;
    }

    public BoardModel StopBoard(long boardId)
    {
        return boardDataService.DeleteBoard(boardId).Result;
    }

    public BoardModel DeleteBoard(BoardModel boardModel)
    {
        return boardDataService.DeleteBoard(boardModel.BoardId).Result;
    }

    public BoardModel UpdateBoard(long boardId,BoardModel boardModel)
    {
        return boardDataService.UpdateBoard(boardId,boardModel).Result;
    }

    public BoardModel GetBoard(long boardId)
    {
        return boardDataService.GetById(boardId).Result;
    }

    public List<BoardModel> GetAllBoards()
    {
        return boardDataService.GetAll().Result;
    }
}
   
