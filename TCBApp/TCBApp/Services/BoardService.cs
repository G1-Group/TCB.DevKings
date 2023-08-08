using TCBApp.Interface;
using TCBApp.Models;
using Telegram.Bot.Types;
using Message = TCBApp.Models.Message;

namespace TCBApp.Services;

public class BoardService:IBoardInterface
{

    private BoardDataService boardDataService;
    private MessageDataService messageDataService;

    public BoardService()
    {
        boardDataService = new BoardDataService(DBConnection.connection);
        messageDataService = new MessageDataService(DBConnection.connection);
    }
 

    public Task<int> CreateBoard(BoardModel boardModel)

    {
        return  boardDataService.Insert(boardModel);
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
    
    public async Task<Message>ChangeBoardMessageStatus(long messageId,Message message)
    {
        return messageDataService.UpdateMessage(messageId, message).Result;
    }

    public async Task<BoardModel> StopBoard(long boardId)
    {
        return boardDataService.DeleteBoard(boardId).Result;
    }

    public async Task<BoardModel> DeleteBoard(long id)
    {
        return boardDataService.DeleteBoard(id).Result;
    }

    public List<BoardModel> GetBoardFromUserId(long userId)
    {
        var list = GetAllBoards();
        return list.Result.Where(x => x.OwnerId == userId).ToList();
    }

    public async Task<BoardModel> UpdateBoard(long boardId,BoardModel boardModel)
    {
        return boardDataService.UpdateBoard(boardId,boardModel).Result;
    }

    public async Task<BoardModel> GetBoard(long boardId)
    {
        return boardDataService.GetById(boardId).Result;
    }

    public async Task<List<BoardModel>> GetAllBoards()
    {
        return boardDataService.GetAll().Result;
    }

    public async Task<BoardModel> FindBoardByNickName(string nickName)
    {
        return  boardDataService.GetAll().Result.Find(x=>x.NickName == nickName);
    }
}
   
