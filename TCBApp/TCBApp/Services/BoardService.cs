using Microsoft.EntityFrameworkCore;
using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services.DataService;
using Message = TCBApp.Models.Message;

namespace TCBApp.Services;

public class BoardService : IBoardService
{

    public readonly BoardDataService _boardDataService;
    public readonly MessageDataService _messageDataService;

    public BoardService(BoardDataService boardDataService, MessageDataService messageDataService)
    {
        _boardDataService = boardDataService;
        _messageDataService = messageDataService;
    }
 

    public async Task<BoardModel> CreateBoard(BoardModel boardModel)
    {
        return  await _boardDataService.AddAsync(boardModel);
    }

    public async Task<List<Message>> GetBoardMessages(long boardId)
    {
        return await _messageDataService.GetAll().Where(x => x.BoardId == boardId).ToListAsync();
    }
    public async Task<BoardModel> CreateNewBoard(long ownerId,string nickName)
    {
        BoardModel boardModel = new BoardModel()
        {
           OwnerId = ownerId,
           NickName = nickName
        };
         var insertedBoard = await _boardDataService.AddAsync(boardModel);
         if (insertedBoard is BoardModel)
             return insertedBoard;

         throw new Exception("Unable to create new board");
    }
    
    // public async Task<Message> ChangeBoardMessageStatus(long messageId, MessageStatus messageStatus)
    // {
    //     return _messageDataService.UpdateMessage(messageId, message).Result;
    // }

    public async Task SetMessageStatusAsRead(long messageId)
    {
        var message = await _messageDataService
            .GetByIdAsync(messageId);

        if (message is null)
            throw new Exception("Message not found");
        
        message.MessageStatus = MessageStatus.Read;

        await _messageDataService.UpdateAsync(message);
    }

    public async Task<List<BoardModel>> GetBoardFromUserId(long userId)
    {
        return await _boardDataService.GetAll()
            .Where(x => x.OwnerId == userId)
            .ToListAsync();
    }
    

    public async Task<BoardModel?> FindBoardByNickName(string nickName)
    {
        return await _boardDataService
            .GetAll()
            .FirstOrDefaultAsync(x => x.NickName == nickName);
    }
}
   
