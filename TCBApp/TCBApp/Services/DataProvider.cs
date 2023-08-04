using TCBApp.Models;

namespace TCBApp.Services;

public partial class DataProvider
{
    private List<BoardModel> Boards = new List<BoardModel>();
    public BoardModel CreateBoard(BoardModel boardModel)
    {
        bool IsExisting = CheckExistBoard(boardModel.BoardId);
        if (IsExisting)
        {
            Boards.Add(boardModel);
            return boardModel;
        }
        throw new Exception("Board all ready exist");
    }
    public BoardModel StopBoard(long boarId)
    {
        var board = GetBoard(boarId);
        if (board is not null)
        {
            Boards.Remove(board);
            return board;
        }
        throw new Exception("Board is not found");
    }

    public BoardModel UpdateBoard(BoardModel boardModel)
    {
        Boards.Remove(Boards.Find(x => x.BoardId == boardModel.BoardId));
        Boards.Add(boardModel);
        return boardModel;
    }
    public BoardModel DeleteBoard(BoardModel boardModel)
    {
        if (CheckExistBoard(boardModel.BoardId))
        {
            Boards.Remove(boardModel);
            
            return boardModel;
        }
        throw new Exception("Board is not found");
    }
    public BoardModel GetBoard(long boardId)
    {
        return Boards.Find(x => x.BoardId == boardId);
    }
    public List<BoardModel> GetAllBoards()
    {
        return Boards;
    }

    public bool CheckExistBoard(long boardId)
    {
        var board = Boards.Find(x => x.BoardId == boardId);
        if (board is null)
        {
            return false;
        }

        return true;
    }
}