using TCBApp.Models;

namespace TCBApp.Services.Interface;

public interface IMessageService
{
    Task<Message> SendMessageToBoard(SendMessageToBoardViewModel model);
    Task<List<Message>> GetBoardMessages(long boardId);
}