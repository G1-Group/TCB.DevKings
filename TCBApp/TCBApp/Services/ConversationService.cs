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
     return  _dataProvider.CreateConversation(chat);
    }

    public ChatModel StopConversation(long chatId)
    {
        return _dataProvider.StopConversation(chatId);
    }

    public ChatModel GetLastConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public ChatModel UpdateConversation(ChatModel chat)
    {
        return _dataProvider.UpdateConversation(chat);
    }

    public ChatModel GetConversation(long chatId)
    {
        return _dataProvider.GetConversation(chatId);
    }

    public List<ChatModel> GetAllConversation()
    {
        return _dataProvider.GetAllConversation();
    }
}