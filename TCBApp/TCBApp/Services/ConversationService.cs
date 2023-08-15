using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services.DataService;

namespace TCBApp.Services;

public class ConversationService : IConversationService
{
    private readonly ConversationDataService _conversationDataService;

    public ConversationService(ConversationDataService conversationDataService)
    {
        _conversationDataService = conversationDataService;
    }


    public int CreateConversation(ChatModel chat)
    {
        throw new NotImplementedException();
    }

    public async Task StopConversation(long chatId)
    {
        var conversation = await this
            ._conversationDataService
            .GetByIdAsync(chatId);
        
        if (conversation is null)
            throw new Exception("Conversation not found!");
        
        conversation.State = ChatState.Closed;
        await _conversationDataService.UpdateAsync(conversation);
    }
}