using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Interface;

public interface IBoardInterface
{
  public int CreateBoard(BoardModel boardModel);
  public BoardModel StopBoard(long boardId);
  public BoardModel DeleteBoard(BoardModel boardModel);
  public BoardModel UpdateBoard(long boardId,BoardModel boardModel);
  public BoardModel GetBoard(long boardId);
  public List<BoardModel> GetAllBoards();
  
}