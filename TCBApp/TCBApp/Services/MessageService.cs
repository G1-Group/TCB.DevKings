using TCBApp.Models;

namespace TCBApp.Services;

public class MessageService
{
    private MessageDataService _messageDataService;

    public MessageService(MessageDataService messageDataService)
    {
        _messageDataService = messageDataService;
    }

    public async Task<int> SendMessageToBoard(Message message)
    {
        return await _messageDataService.Insert(message);
    }

    public async Task<List<Message>> GetBoardMessages(long boardId)
    {
        return await _messageDataService.GetByIdFromBoard(boardId);
    }
    
}