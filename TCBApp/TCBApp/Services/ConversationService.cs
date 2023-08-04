using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class ConversationService:IConversationInterface
{
    private DataProvider _dataProvider { get; set; }

    public ConversationService(DataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }
    public ChatModel CreateConversation(ChatModel chat)
    {
        throw new NotImplementedException();
    }

    public ChatModel StopConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public ChatModel GetLastConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public ChatModel UpdateConversation(ChatModel chat)
    {
        throw new NotImplementedException();
    }

    public ChatModel GetConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public List<ChatModel> GetAllConversation()
    {
        throw new NotImplementedException();
    }
}