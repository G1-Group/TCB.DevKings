using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services.DataService;
using TCBApp.Services.Interface;

namespace TCBApp.Services;

public class MessageService : IMessageService
{
    private MessageDataService _messageDataService;

    public MessageService(MessageDataService messageDataService)
    {
        _messageDataService = messageDataService;
    }

    public async Task<Message> SendMessageToBoard(SendMessageToBoardViewModel model)
    {
        var message = new Message()
        {
            FromId = model.FromId,
            BoardId = model.BoardId,
            MessageStatus = MessageStatus.NotRead,
            MessageType = MessageType.BoardMessage,
            Content = model.Content
        };

        return await _messageDataService.AddAsync(message);
    }

    public async Task<List<Message>> GetBoardMessages(long boardId)
    {
        return await _messageDataService
            .GetAll()
            .Where(x => x.BoardId == boardId && x.MessageStatus == MessageStatus.NotRead)
            .ToListAsync();
    }

    public Task<Message> SendMessageToConversation(SendMessageToConversationViewModel model)
    {
        var message = new Message
        {
            FromId = model.FromId,
            Client = null,
            Content = model.Content,
            ConversationId = model.ConversationId,
            Conversation = null,
            BoardId = null,
            Board = null,
            MessageType = MessageType.AnonymChatMessage,
            MessageStatus = MessageStatus.NotRead
        };
     return  _messageDataService.AddAsync(message);
    }

   
}