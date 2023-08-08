using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class ConversationService:IConversationInterface
{
    private ConversationDataService Service { get; set; }

    public ConversationService()
    {
        Service = new ConversationDataService(DBConnection.connection);
    }
    public int CreateConversation(ChatModel chat)
    {
       return Service.Insert(chat).Result;
    }

    public ChatModel StopConversation(long chatId)
    {
        return Service.StopConversation( chatId).Result;
    }

    public ChatModel GetLastConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public ChatModel UpdateConversation(ChatModel chat)
    {
        return Service.UpdateConversation(chat.Id, chat).Result;
    }

    public ChatModel GetConversation(long chatId)
    {
        return Service.GetById(chatId).Result;
    }

    public List<ChatModel> GetAllConversation()
    {
        return Service.GetAll().Result;
    }
}