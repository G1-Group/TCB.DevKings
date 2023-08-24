using TCBApp.Models;

namespace TCBApp.Services;

public interface IBoardService
{
    Task<BoardModel> CreateBoard(BoardModel boardModel);
    Task<List<Message>> GetBoardMessages(long boardId);
    Task<BoardModel> CreateNewBoard(long ownerId,string nickName);
    Task SetMessageStatusAsRead(long messageId);
    Task<List<BoardModel>> GetBoardFromUserId(long userId);
    Task<BoardModel?> FindBoardByNickName(string nickName);
}