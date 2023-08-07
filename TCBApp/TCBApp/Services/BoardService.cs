using TCBApp.Interface;
using TCBApp.Models;
using Telegram.Bot.Types;
using Message = TCBApp.Models.Message;

namespace TCBApp.Services;

public class BoardService:IBoardInterface
{

    private BoardDataService boardDataService;
    private MessageDataService messageDataService;
    public int CreateBoard(BoardModel boardModel)
    {
        return boardDataService.Insert(boardModel).Result;
    }

    public async Task<List<Message>> GetBoardMessages(long boardId)
    {
        var res = messageDataService.GetByIdFromBoard(boardId).Result;
        return res;
    }
    public async Task<BoardModel> CreateNewBoard(long ownerId,string nickName)
    {
        BoardModel boardModel = new BoardModel()
        {
           OwnerId = ownerId,
           NickName = nickName
        };
         boardDataService.Insert(boardModel);
         throw new Exception("Error CreateNewBoard@ ");
    }
    
    public Message ChangeBoardMessageStatus(long messageId,Message message)
    {
        return messageDataService.UpdateMessage(messageId, message).Result;
    }

    public BoardModel StopBoard(long boardId)
    {
        return boardDataService.DeleteBoard(boardId).Result;
    }

    public BoardModel DeleteBoard(long id)
    {
        return boardDataService.DeleteBoard(id).Result;
    }

    public List<BoardModel> GetBoardFromUserId(long userId)
    {
        var list = GetAllBoards();
        return list.Where(x => x.OwnerId == userId).ToList();
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

    public BoardModel FindBoardByNickName(string nickName)
    {
        return boardDataService.GetAll().Result.Find(x=>x.NickName == nickName);
    }
}
   
