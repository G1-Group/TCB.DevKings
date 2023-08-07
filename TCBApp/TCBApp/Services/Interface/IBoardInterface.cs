using TCBApp.Models;
using Telegram.Bot.Types;
using Message = TCBApp.Models.Message;

namespace TCBApp.Interface;

public interface IBoardInterface
{
  public int CreateBoard(BoardModel boardModel);
  public Task<BoardModel> CreateNewBoard(long ownerId,string nickName);
  public Message ChangeBoardMessageStatus(long messageId,Message message);
  public BoardModel StopBoard(long boardId);
  public BoardModel DeleteBoard(long id);
  public BoardModel UpdateBoard(long boardId,BoardModel boardModel);
  public BoardModel GetBoard(long boardId);
  public List<BoardModel> GetAllBoards();
  public BoardModel FindBoardByNickName(string nickName);
}