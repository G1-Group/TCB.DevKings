using TCBApp.Models;
using Telegram.Bot.Types;
using Message = TCBApp.Models.Message;

namespace TCBApp.Interface;

public interface IBoardInterface
{
  public Task<int> CreateBoard(BoardModel boardModel);
  public Task<BoardModel> CreateNewBoard(long ownerId,string nickName);
  public Task<Message> ChangeBoardMessageStatus(long messageId,Message message);
  public Task<BoardModel> StopBoard(long boardId);
  public Task<BoardModel> DeleteBoard(long id);
  public Task<BoardModel> UpdateBoard(long boardId,BoardModel boardModel);
  public Task<BoardModel> GetBoard(long boardId);
  public Task<List<BoardModel>> GetAllBoards();
  public Task<BoardModel> FindBoardByNickName(string nickName);
}